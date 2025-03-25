using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudyPractice.Models;

public partial class Employee
{
    public int EmployeId { get; set; }

    public string EmployeFio { get; set; } = null!;

    public int? DepartmentId { get; set; }

    public string? EmployePhone { get; set; }

    public int? PositionId { get; set; }

    public int? OfficeId { get; set; }

    public string? EmployeEmail { get; set; }

    public DateOnly EmployeBirth { get; set; }

    public int? EmployeAssistentId { get; set; }

    public int? EmployeChiefId { get; set; }

    public virtual ICollection<AbsenceCalendar> AbsenceCalendarEmployees { get; set; } = new List<AbsenceCalendar>();

    public virtual ICollection<AbsenceCalendar> AbsenceCalendarReplacements { get; set; } = new List<AbsenceCalendar>();

    public virtual Department? Department { get; set; }

    public virtual Employee? EmployeAssistent { get; set; }

    public virtual Employee? EmployeChief { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Employee> InverseEmployeAssistent { get; set; } = new List<Employee>();

    public virtual ICollection<Employee> InverseEmployeChief { get; set; } = new List<Employee>();

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();

    public virtual Office? Office { get; set; }

    public virtual Position? Position { get; set; }

    public virtual ICollection<TraningCalendar> TraningCalendars { get; set; } = new List<TraningCalendar>();
}
