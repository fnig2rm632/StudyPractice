using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using ReactiveUI;
using StudyPractice.Context;
using StudyPractice.Models;

namespace StudyPractice.ViewModels;

public class HierarchyWindowModel : ReactiveObject, IRoutableViewModel
{
    public ObservableCollection<Controls> Items { get; set; } = new();
    public int TreePanelHeight { get; set; } = 2300;
    public int TreePanelWidth { get; set; } = 2300;
    public IScreen HostScreen { get; }

    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
    
    public ObservableCollection<TreeNodeViewModel> Nodes { get; } = new()
    {
        new TreeNodeViewModel { Text = "Root", X = 350, Y = 50 },
        new TreeNodeViewModel { Text = "Child 1", X = 200, Y = 150 },
        new TreeNodeViewModel { Text = "Child 2", X = 500, Y = 150 },
        new TreeNodeViewModel { Text = "Leaf 1", X = 150, Y = 250 },
        new TreeNodeViewModel { Text = "Leaf 2", X = 250, Y = 250 }
    };

    // Список связей (линий)
    public ObservableCollection<ConnectionViewModel> Connections { get; } = new()
    {
        new ConnectionViewModel { StartPoint = new Point(390, 70), EndPoint = new Point(240, 150) },
        new ConnectionViewModel { StartPoint = new Point(390, 70), EndPoint = new Point(540, 150) },
        new ConnectionViewModel { StartPoint = new Point(240, 170), EndPoint = new Point(190, 250) },
        new ConnectionViewModel { StartPoint = new Point(240, 170), EndPoint = new Point(290, 250) }
    };
    public int PanelCount => 2;
    public bool IsVisibleListPeople => false;
    public ReactiveCommand<Unit, IRoutableViewModel> GoInformation { get; }
    public HierarchyWindowModel(IScreen screen)
    {
        HostScreen = screen;
        GoInformation = ReactiveCommand.CreateFromObservable(() =>
            screen.Router.Navigate.Execute(new InformationWindowModel(screen)));
        DrawTree();
    }

    private void DrawTree()
    {
        List<Employee> employees = new();
        List<Department> departments = new();
            
        using (var context = new TesterContext())
        {
            employees = context.Employees.ToList();
            departments = context.Departments.ToList();
        }
        
        Dictionary<int,int> listNodes = CreateNodes(employees, departments);

        

        
    }
    
    public class TreeNodeViewModel : ReactiveObject
    {
        public string Text { get; set; } = "";
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class ConnectionViewModel : ReactiveObject
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
    }

    private static Dictionary<int, int> CreateNodes( List<Employee> employees, List<Department> departments)
    {
        return new Dictionary<int,int>();
    }
}