using System;
using System.Collections.Generic;

namespace StudyPractice.Models;

public partial class TraningCalendar
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Classification { get; set; }

    public DateOnly DateStart { get; set; }

    public DateOnly DateFinish { get; set; }

    public int? MaterialId { get; set; }

    public int EmployeId { get; set; }

    public virtual Employee Employe { get; set; } = null!;

    public virtual Material? Material { get; set; }
}
