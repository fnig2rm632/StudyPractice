using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia;
using Avalonia.Media;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using StudyPractice.Context;
using StudyPractice.Models;
using StudyPractice.Views;

namespace StudyPractice.ViewModels;

public class HierarchyWindowModel : ReactiveObject, IRoutableViewModel
{
    #region TreePanelHeightAndWidth

    public int TreePanelHeight { get; set; } = 2300;

    public int TreePanelWidth { get; set; } = 2300;

    #endregion

    #region Nodes/Departments/EmployeesList

    private List<Node> NodesList { get; set; } = new();

    private List<Department> Departments { get; set; } = new();

    private List<Employee> _employees = new();

    private List<Employee> EmployeesList { get; set; } = new();

    public List<Employee> Employees
    {
        get => _employees;
        set => this.RaiseAndSetIfChanged(ref _employees, value);
    }

    #endregion

    #region RoutingWindow

    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

    public ReactiveCommand<Unit, IRoutableViewModel> GoInformation { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoEmployee { get; }

    public IScreen HostScreen { get; }

    #endregion

    #region DisideBug

    private int CountLine { get; set; }

    #endregion

    #region OpenListDepartment

    public ReactiveCommand<Unit, Unit> ClickCommand { get; } = null!;

    private string SelectedDepartment { get; set; } = null!;



    #endregion

    #region ObservableCollection<Controls>

    public ObservableCollection<TreeNodeViewModel> Nodes { get; } = new();

    public ObservableCollection<ConnectionViewModel> Connections { get; } = new();

    #endregion

    #region PanelCount

    private int _panelCount = 2;

    public int PanelCount
    {
        get => _panelCount;
        set => this.RaiseAndSetIfChanged(ref _panelCount, value);
    }

    #endregion

    #region IsVisibleListPeople

    private bool _isVisibleListPeople;

    public bool IsVisibleListPeople
    {
        get => _isVisibleListPeople;
        set => this.RaiseAndSetIfChanged(ref _isVisibleListPeople, value);
    }

    #endregion

    // Data Type
    public HierarchyWindowModel(IScreen screen)
    {
        HostScreen = screen;
        
        GoEmployee = ReactiveCommand.CreateFromObservable(() =>
            screen.Router.Navigate.Execute(new EmployeeWindowViewModel(screen)));
        
        GoInformation = ReactiveCommand.CreateFromObservable(() =>
            screen.Router.Navigate.Execute(new InformationWindowModel(screen)));
        
        DrawTree();
    }

    // Класс кнопки ноды
    public class TreeNodeViewModel : ReactiveObject
    {
        public string Text { get; set; } = "";
        public double X { get; set; }
        public double Y { get; set; }
        public SolidColorBrush Bg { get; set; } = new SolidColorBrush(Color.Parse("#E1F4C8"));
        public SolidColorBrush Fg { get; set; } = new SolidColorBrush(Colors.Black);
        public ReactiveCommand<Unit, Unit> ClickCommand { get; }

        public TreeNodeViewModel(Action<TreeNodeViewModel> onClickAction)
        {
            ClickCommand = ReactiveCommand.Create(() => onClickAction(this));
        }
    }

    // Линия между нодами
    public class ConnectionViewModel : ReactiveObject
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
    }

    // Рисовка дерева дипортаментов
    private void DrawTree()
    {
        // Взятие дипортаментов из базы
        using (var context = new TesterContext())
        {
            Departments = context.Departments.ToList();
            EmployeesList = context.Employees.Include(x => x.Department)
                .Include(x => x.Office)
                .Include(x => x.Position).ToList();
        }

        // Сохранение и Рисоваение Nodes and Line
        NodesList = DrawNodes(CreateNodes(Departments), 0, 0);

        // Передача шарины
        TreePanelWidth = NodesList.Sum(n => n.Weight);

    }

    // Рисование веток ноды
    private List<Node> DrawNodes(List<Node> nodes, int leftBlockWidth, int level, Node? parent = null)
    {
        foreach (var node in nodes)
        {
            node.X = leftBlockWidth + node.Weight / 2 - 160;
            node.Y = level * 150 + 50;
            SolidColorBrush bg = new SolidColorBrush(Color.Parse("#E1F4C8"));
            SolidColorBrush fg = new SolidColorBrush(Colors.Black);

            // Создание дитей если они есть
            if (node.Children.Count != 0)
            {
                node.Children = DrawNodes(node.Children, leftBlockWidth, level + 1, node);
            }

            // Выбраная кнопка
            if (node.Name == SelectedDepartment)
            {
                bg = new SolidColorBrush(Color.Parse("#78B24B"));
                fg = new SolidColorBrush(Colors.White);
            }

            // Кнопка корнивая нода
            var buttonNode = new TreeNodeViewModel(OnNodeClick)
                { Text = node.Name, X = node.X, Y = node.Y, Bg = bg, Fg = fg };

            // Создание связи если есть прародитель
            if (parent != null)
            {
                CreateLine(parent, node);
            }

            // Добавление Нады
            Nodes.Add(buttonNode);
            leftBlockWidth += node.Weight;
        }

        return nodes;
    }

    // Перезагрузка TreeDraw
    private void ReloadDrawTree(List<Node>? nodes = null)
    {
        if (nodes == null)
        {
            // Очищение листа нод
            Nodes.Clear();
            // Если начинаюся корнивые ноды
            nodes = NodesList;
        }

        foreach (var node in nodes)
        {
            SolidColorBrush bg = new SolidColorBrush(Color.Parse("#E1F4C8"));
            SolidColorBrush fg = new SolidColorBrush(Colors.Black);

            // Выбраная кнопка
            if (node.Name == SelectedDepartment)
            {
                bg = new SolidColorBrush(Color.Parse("#78B24B"));
                fg = new SolidColorBrush(Colors.White);
            }

            // Отрисовка детей
            if (node.Children.Count != 0)
            {
                ReloadDrawTree(node.Children);
            }

            // Добавление нод
            Nodes.Add(new TreeNodeViewModel(OnNodeClick)
                { Text = node.Name, X = node.X, Y = node.Y, Bg = bg, Fg = fg });
        }
    }

    // Создание Связи с родителем
    private void CreateLine(Node parent, Node node)
    {
        // По Кардинатам x
        var mainX = Convert.ToDouble(node.X + 160);
        var parentX = Convert.ToDouble(parent.X + 160);

        // По Кардинатам y
        var mainY = node.Y - 350;
        var parentY = parent.Y + 50 - 350;

        // Пошли в Ж*пу разрабы авалони
        if (CountLine < 5)
        {
            switch (CountLine)
            {
                case 0:
                    mainY = node.Y;
                    parentY = parent.Y + 50;
                    break;
                case 4:
                    mainY = 150;
                    parentY = 50;
                    break;
                default:
                    mainY = node.Y - 200;
                    parentY = parent.Y - 150;
                    break;
            }
        }

        var line = new ConnectionViewModel
        {
            StartPoint = new Point(parentX, parentY),
            EndPoint = new Point(mainX, mainY)
        };

        Connections.Add(line);
        CountLine += 1;
    }

    // Создание Списка корнивых нод
    private List<Node> CreateNodes(List<Department> departments)
    {
        List<Node> nodes = new();

        while (departments.Count != 0)
        {
            // Нахождение дипортамента с наименьшим индексом 
            var department = departments.Aggregate((minDept, currentDept) =>
                currentDept.DepartmentId < minDept.DepartmentId ? currentDept : minDept);

            // Создание корневой ноды 
            var departmentTemp = CreateNode(departments, department);

            // Измемения связи с созданием корневой ноды
            departments = departmentTemp.Item2;

            // Добавление корневой ноды 
            nodes.Add(departmentTemp.Item1);
        }

        return nodes;
    }

    // Рекурсивный метод создания ноды
    private (Node, List<Department>) CreateNode(List<Department> departments, Department department)
    {
        // Сохранения индекса и удаление дипортамента из листа
        var departmentIndex = Convert.ToInt32(department.DepartmentId);
        departments.Remove(department);

        // Создание ноды
        Node node = new() { Name = department.DepartmentName };

        // Проверка на то есть ли у ноды дети
        if (departments.Any(x => x.DepartmentHeadId == departmentIndex))
        {
            var childrenDepartments = departments.Where(x => x.DepartmentHeadId == departmentIndex).ToList();

            foreach (var childDepartment in childrenDepartments)
            {
                // Рекурсия для создания дитей ноды
                var nodeTemp = CreateNode(departments, childDepartment);
                departments = nodeTemp.Item2;
                node.Children.Add(nodeTemp.Item1);
            }
        }

        return (node, departments);
    }

    // Нажатие на Депортамент
    private void OnNodeClick(TreeNodeViewModel node)
    {
        if (SelectedDepartment == node.Text)
        {
            // Если кнопка уже выбрана
            PanelCount = 2;
            IsVisibleListPeople = false;
            SelectedDepartment = "";

        }
        else
        {
            // Если кнопка выбрана
            SelectedDepartment = node.Text;
            PanelCount = 1;
            IsVisibleListPeople = true;
            Employees = SelectedEmployees();
        }

        ReloadDrawTree();
    }

    // Выбор людей по выбранному Депоратамунту
    private List<Employee> SelectedEmployees()
    {
        List<Employee> employees = new List<Employee>();
        List<Node> nodes = [FindNode(SelectedDepartment, NodesList)!];
        List<string> departments = FindInsideDeportment(nodes).Select(x => x.Name).ToList();

        foreach (var employee in EmployeesList)
        {
            if (departments.Any(x => x == employee.Department!.DepartmentName))
            {
                employees.Add(employee);
            }
        }

        return employees;
    }

    // Поиск дипортаментов по другим дипортаментам
    private List<Node> FindInsideDeportment(List<Node>? childen)
    {
        List<Node> nodes = new();

        if (childen!.Count != 0)
        {
            foreach (var child in childen)
            {
                if (child.Children.Count != 0)
                {
                    nodes.AddRange(FindInsideDeportment(child.Children));
                }

                nodes.Add(child);
            }
        }

        return nodes;
    }

    // Рекурсивный метод Поиска ноды по имени
    private Node? FindNode(string findNode, List<Node>? child)
    {
        if (child!.Count > 0)
        {
            foreach (var cNode in child)
            {
                if (cNode.Children.Count > 0)
                {
                    var node = FindNode(findNode, cNode.Children);
                    if (node != null)
                    {
                        return node;
                    }
                }

                if (cNode.Name == findNode)
                {
                    return cNode;
                }
            }
        }

        return null;
    }

}
