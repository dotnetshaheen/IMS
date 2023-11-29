using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string LoginName { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;

    public string UserMobileNumber { get; set; } = null!;

    public int? RoleId { get; set; }

    public int IsActive { get; set; }

    public int IsDeleted { get; set; }

    public DateTime CreationTime { get; set; }

    public int? CreatorUserId { get; set; }

    public DateTime? LastModificationTime { get; set; }

    public int? ModifierUserId { get; set; }

    public DateTime? DeletationTime { get; set; }

    public int? DeletorUserId { get; set; }
}
