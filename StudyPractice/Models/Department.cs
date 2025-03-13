using System;
using System.Collections.Generic;

namespace StudyPractice.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string? DepartmentDescription { get; set; }

    public int? DepartmentHeadId { get; set; }

    public virtual Department? DepartmentHead { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Department> InverseDepartmentHead { get; set; } = new List<Department>();
}
