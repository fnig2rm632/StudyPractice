using System;
using System.Collections.Generic;

namespace StudyPractice.Models;

public partial class Status
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}
