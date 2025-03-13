using System;
using System.Collections.Generic;

namespace StudyPractice.Models;

public partial class TypesMaterial
{
    public int TypeMaterialId { get; set; }

    public string TypeMaterialName { get; set; } = null!;

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}
