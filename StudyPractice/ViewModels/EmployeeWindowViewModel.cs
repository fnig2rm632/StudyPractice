using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using ReactiveUI;
using StudyPractice.Context;
using StudyPractice.Models;

namespace StudyPractice.ViewModels;

public class EmployeeWindowViewModel : ReactiveObject, IRoutableViewModel
{
    #region RoutingState
    
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; }
        
        private EditMode _mode;
        public EditMode Mode
        {
            get => _mode;
            set
            {
                this.RaiseAndSetIfChanged(ref _mode, value);
            }
        }

        public ReactiveCommand<Unit, Unit> SaveCommand { get; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }

    #endregion

    #region Date Office

        private List<Office>? _offices;
        public List<Office>? Offices
        {
            get => _offices;
            set
            {
                this.RaiseAndSetIfChanged(ref _offices, value);
            }
        }
        private Office? _selectedOffices;
        
        public Office? SelectedOffice
        {
            get
            {
                if (EmployeeSelect.OfficeId != null)
                {
                    return Offices?.FirstOrDefault(x => x.OfficeId == EmployeeSelect.Office.OfficeId);
                }
        
                
                return Offices?.FirstOrDefault();
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedOffices, value);
            }
        }

        #endregion
    
    #region Staff

    private List<Employee>? _staffs;
    public List<Employee>? Staffs
    {
        get => _staffs;
        set
        {
            this.RaiseAndSetIfChanged(ref _staffs, value);
        }
    }
    private Employee? _employeChief;
        
    public Employee? EmployeChief
    {
        get => Staffs[0];
        set
        {
            this.RaiseAndSetIfChanged(ref _employeChief, value);
        }
    }
    
    private Employee? _employeAssistant;
        
    public Employee? EmployeAssistant
    {
        get =>  Staffs[0];
        set
        {
            this.RaiseAndSetIfChanged(ref _employeAssistant, value);
        }
    }

    #endregion

    #region Calendar

    private DateOnly _dateOnlyValue;
    private DateTime? _selectedDateTime;

    public DateOnly DateOnlyValue
    {
        get => _dateOnlyValue;
        set
        {
            if (_dateOnlyValue != value) // Предотвращаем бесконечную рекурсию
            {
                _dateOnlyValue = value;
                this.RaisePropertyChanged();
                _selectedDateTime = value.ToDateTime(TimeOnly.MinValue);
                this.RaisePropertyChanged(nameof(SelectedDateTime));
            }
        }
    }

    public DateTime? SelectedDateTime
    {
        get => _selectedDateTime;
        set
        {
            if (_selectedDateTime != value) // Предотвращаем бесконечную рекурсию
            {
                _selectedDateTime = value;
                this.RaisePropertyChanged();
                if (value.HasValue)
                {
                    _dateOnlyValue = DateOnly.FromDateTime(value.Value);
                    this.RaisePropertyChanged(nameof(DateOnlyValue));
                }
            }
        }
    }

    #endregion

    #region Employ

        private Employee _employeeSelect = new();
        public Employee EmployeeSelect
        {
            get => _employeeSelect;
            set
            {
                this.RaiseAndSetIfChanged(ref _employeeSelect, value);
                
                DateOnlyValue = EmployeeSelect.EmployeBirth;
                SelectedOffice = EmployeeSelect.Office;

            }
        }

    #endregion

    #region Error

    private bool _error1;
    public bool Error1
    {
        get => _error1;
        set
        {
            this.RaiseAndSetIfChanged(ref _error1, value);
        }
    }
    
    private bool _error2;
    public bool Error2
    {
        get => _error2;
        set
        {
            this.RaiseAndSetIfChanged(ref _error2, value);
        }
    }
    
    private bool _error3;
    public bool Error3
    {
        get => _error3;
        set
        {
            this.RaiseAndSetIfChanged(ref _error3, value);
        }
    }
    
    private bool _error4;
    public bool Error4
    {
        get => _error4;
        set
        {
            this.RaiseAndSetIfChanged(ref _error4, value);
        }
    }

    private bool _error5;
    public bool Error5
    {
        get => _error5;
        set
        {
            this.RaiseAndSetIfChanged(ref _error5, value);
        }
    }

    #endregion
    
    public EmployeeWindowViewModel(IScreen screen)
    {
        HostScreen = screen;
        
        SaveCommand = ReactiveCommand.Create(Save);
        AddCommand = ReactiveCommand.Create(Add);
        BackCommand = ReactiveCommand.Create(Back);

        TakeDate();
        
        
    }

    private void TakeDate()
    {
        var listStaffs = new List<Employee>(){new Employee(){EmployeId = 0,EmployeFio = "Не указанно"}};
        using (var context = new TesterContext())
        {
            Offices = context.Offices.ToList();
            listStaffs.AddRange(context.Employees.Where(x => x.EmployeId != EmployeeSelect.EmployeId));
            Staffs = listStaffs;
        }
        
    }

    private void Add()
    {
        if (IsEmployees())
        {
            var tempEmployees = EmployeeSelect;
            tempEmployees.OfficeId = SelectedOffice.OfficeId;
            tempEmployees.EmployeBirth = DateOnlyValue;
            tempEmployees.DepartmentId = 129;

            using (var context = new TesterContext())
            {
                context.Employees.Add(tempEmployees);
                context.SaveChanges();

            }
            Back();
        }
    }

    private void Save()
    {
        if (IsEmployees())
        {
            var tempEmployees = EmployeeSelect;
            tempEmployees.OfficeId = SelectedOffice.OfficeId;
            tempEmployees.EmployeBirth = DateOnlyValue;
            using (var context = new TesterContext())
            {
                context.Employees.Update(tempEmployees);
                context.SaveChanges();
            }
            Back();
        }
    }

    private bool IsEmployees()
    {
        if (!string.IsNullOrEmpty(EmployeeSelect.EmployeFio))
        {
            Error1 = false;
            if (!string.IsNullOrEmpty(EmployeeSelect.EmployePhone))
            {
                Error2 = false;
                if (!string.IsNullOrEmpty(EmployeeSelect.EmployeEmail))
                {
                    Error3 = false;
                    if (SelectedDateTime != null)
                    {
                        Error4 = false;
                        if (SelectedOffice != null && SelectedOffice.OfficeId > 0)
                        {
                            return true;
                        }
                        Error5 = true;
                        return false;
                    }
                    Error4 = true;
                    return false;
                }
                Error3 = true;
                return false;
            }
            Error2 = true;
            return false;
        }
        Error1 = true;
        return false;
    }
    
    private void Back() => HostScreen.Router.Navigate.Execute(new HierarchyWindowModel(HostScreen));
}