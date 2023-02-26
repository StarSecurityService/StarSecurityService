using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StarSecurityService.Models;

[Table("Account")]
public partial class Account
{
    [Key]
    [Column("accountID")]
    public int AccountId { get; set; }

    [Column("roleID")]
    public int? RoleId { get; set; }

    [Column("password")]
    [StringLength(255)]
    public string? Password { get; set; }

    [Column("email")]
    [StringLength(255)]
    public string? Email { get; set; }

    [Column("phone")]
    [StringLength(255)]
    public string? Phone { get; set; }

    [Column("firstName")]
    [StringLength(255)]
    public string? FirstName { get; set; }

    [Column("lastName")]
    [StringLength(255)]
    public string? LastName { get; set; }

    [Column("address")]
    [StringLength(255)]
    public string? Address { get; set; }

    [Column("cardID")]
    [StringLength(255)]
    public string? CardId { get; set; }

    [InverseProperty("Account")]
    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    [ForeignKey("RoleId")]
    [InverseProperty("Accounts")]
    public virtual Role? Role { get; set; }
}
