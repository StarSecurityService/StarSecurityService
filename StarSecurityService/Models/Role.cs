﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StarSecurityService.Models;

[Table("Role")]
public partial class Role
{
    [Key]
    [Column("roleID")]
    public int RoleId { get; set; }

    [Column("roleName")]
    [StringLength(1)]
    public string? RoleName { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}
