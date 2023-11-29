using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public int IsActive { get; set; }

    public int IsDeleted { get; set; }

    public int IsGlobalRole { get; set; }

    public DateTime CreationTime { get; set; }

    public int? CreatorUserId { get; set; }

    public DateTime? LastModificationTime { get; set; }

    public int? ModifierUserId { get; set; }

    public DateTime? DeletationTime { get; set; }

    public int? DeletorUserId { get; set; }
}
