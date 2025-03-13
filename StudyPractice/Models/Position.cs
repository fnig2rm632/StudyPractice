using System;
using System.Collections.Generic;

namespace StudyPractice.Models;

public partial class Position
{
    public int PositionId { get; set; }

    public string PositionName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<ResumeLibrary> ResumeLibraries { get; set; } = new List<ResumeLibrary>();
}
