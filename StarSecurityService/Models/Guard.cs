using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StarSecurityService.Models;

[Table("Guard")]
public partial class Guard : BaseEntity
{
    [Key]
    [Column("guardID")]
    public int GuardId { get; set; }

    [Column("firstName")]
    [StringLength(255)]
    public string? FirstName { get; set; }

    [Column("lastName")]
    [StringLength(255)]
    public string? LastName { get; set; }

    [Column("serviceID")]
    public int? ServiceId { get; set; }

    [Column("phone")]
    [StringLength(255)]
    public string? Phone { get; set; }

    [Column("height")]
    [StringLength(255)]
    public string? Height { get; set; }

    [Column("weight")]
    [StringLength(255)]
    public string? Weight { get; set; }

    [Column("status")]
    [StringLength(255)]
    public string? Status { get; set; }

    [Column("avatar")]
    [StringLength(255)]
    public string? Avatar { get; set; }

    [Column("cardID")]
    [StringLength(255)]
    public string? CardId { get; set; }

    [InverseProperty("Guard")]
    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

    [ForeignKey("ServiceId")]
    [InverseProperty("Guards")]
    public virtual Service? Service { get; set; }

    [NotMapped]
    [DisplayName("Upload Avatar")]
    public IFormFile ImageFile { get; set; }
}
