using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using Microsoft.EntityFrameworkCore;
using StudyPractice.ViewModels;
using ReactiveUI;
using StudyPractice.Context;
using StudyPractice.Models;

namespace StudyPractice.ViewModels;

public class InformationWindowModel : ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen { get; }

    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

    #region Employee

    private List<Employee>? _employee;
    public List<Employee>? Employee
    {
        get => _employee;
        set
        {
            this.RaiseAndSetIfChanged(ref _employee, value);
        }
    }
    private List<Employee>? _employeeselect;
    public List<Employee>? EmployeeSelect
    {
        get => _employeeselect;
        set
        {
            this.RaiseAndSetIfChanged(ref _employeeselect, value);
        }
    }
    
    private string? _findtext;

    public string? FindText
    {
        get => _findtext;
        set => this.RaiseAndSetIfChanged(ref _findtext, value);
    }

    bool IsNorm(Employee employee)
    {
        if (FindText != null)
        {
           return employee.EmployeFio.ToLower().Contains(FindText.ToLower());
        }
        return true;
    }

    #endregion
    
    public ReactiveCommand<Unit, IRoutableViewModel> GoBackToHierarchy { get; }
    
    public ReactiveCommand<string, Unit> ProcessTextCommand { get; }
    public InformationWindowModel(IScreen screen) {
        
        HostScreen = screen;
        GoBackToHierarchy = ReactiveCommand.CreateFromObservable(() =>
            screen.Router.Navigate.Execute(new HierarchyWindowModel(screen)));
        
        LoadListes();
        EmployeeSelect = Employee;
        ProcessTextCommand = ReactiveCommand.Create<string>(text =>
        {
            FindText = text;
            if (FindText != null)
            {
                EmployeeSelect = Employee.Where(x => IsNorm(x)).ToList();
            }
            else
            {
                EmployeeSelect = Employee;
            }
        });
        
    }

    private void LoadListes()
    {
        using (var context = new TesterContext())
        {
            Employee = context.Employees.Include(x => x.Position).ToList();
        }
    }
}