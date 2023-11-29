using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class AppFeature
{
    public int Id { get; set; }

    public string FeatureName { get; set; } = null!;

    public string? Description { get; set; }

    public int IsActive { get; set; }

    public int IsDeleted { get; set; }

    public virtual ICollection<Right> Rights { get; set; } = new List<Right>();
}
