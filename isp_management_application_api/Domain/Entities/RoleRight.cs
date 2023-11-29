using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class RoleRight
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public int? RightsId { get; set; }

    public int IsActive { get; set; }

    public int IsDeleted { get; set; }

    public DateTime CreationTime { get; set; }

    public int? CreatorUserId { get; set; }

    public DateTime? LastModificationTime { get; set; }

    public int? ModifierUserId { get; set; }

    public DateTime? DeletationTime { get; set; }

    public int? DeletorUserId { get; set; }
}
