using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Right
{
    public int Id { get; set; }

    public string RightsName { get; set; } = null!;

    public string? Description { get; set; }

    public int AppFeatureId { get; set; }

    public int IsActive { get; set; }

    public int IsDeleted { get; set; }

    public virtual AppFeature AppFeature { get; set; } = null!;
}
