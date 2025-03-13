using System;
using System.Collections.Generic;

namespace StudyPractice.Models;

public partial class Material
{
    public int MaterialId { get; set; }

    public string MaterialName { get; set; } = null!;

    public DateOnly? ApprovalDate { get; set; }

    public DateOnly? ModificationDate { get; set; }

    public int? StatusId { get; set; }

    public int? TypeMaterialId { get; set; }

    public int? AreaId { get; set; }

    public int? Author { get; set; }

    public virtual Area? Area { get; set; }

    public virtual Employee? AuthorNavigation { get; set; }

    public virtual Status? Status { get; set; }

    public virtual ICollection<TraningCalendar> TraningCalendars { get; set; } = new List<TraningCalendar>();

    public virtual TypesMaterial? TypeMaterial { get; set; }
}
