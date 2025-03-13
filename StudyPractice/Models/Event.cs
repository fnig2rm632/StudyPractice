using System;
using System.Collections.Generic;

namespace StudyPractice.Models;

public partial class Event
{
    public int EventId { get; set; }

    public string EventName { get; set; } = null!;

    public int? TypeEventId { get; set; }

    public int StatusEventsId { get; set; }

    public DateTime? DateTime { get; set; }

    public int? EventResponsible { get; set; }

    public string? EventDescription { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Employee? EventResponsibleNavigation { get; set; }
}
