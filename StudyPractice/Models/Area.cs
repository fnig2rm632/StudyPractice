using System;
using System.Collections.Generic;

namespace StudyPractice.Models;

public partial class Area
{
    public int AreaId { get; set; }

    public string AreaName { get; set; } = null!;

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}
