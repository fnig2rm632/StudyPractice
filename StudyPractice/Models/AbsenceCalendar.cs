using System;
using System.Collections.Generic;

namespace StudyPractice.Models;

public partial class AbsenceCalendar
{
    public int AbsenceCalendarId { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string AbsenceCalendarResone { get; set; } = null!;

    public int? ReplacementId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Employee? Replacement { get; set; }
}
