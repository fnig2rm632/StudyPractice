using System;
using System.Collections.Generic;

namespace StudyPractice.Models;

public partial class Office
{
    public int OfficeId { get; set; }

    public string OfficeNumder { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
