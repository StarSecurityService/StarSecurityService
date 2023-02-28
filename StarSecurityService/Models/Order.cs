using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StarSecurityService.Models;

[Table("Order")]
public partial class Order : BaseEntity
{
    [Key]
    [Column("orderID")]
    public int OrderId { get; set; }

    [Column("serviceID")]
    public int? ServiceId { get; set; }

    [Column("accountID")]
    public int? AccountId { get; set; }

    [Column("amount")]
    public int? Amount { get; set; }

    [Column("duration")]
    public int? Duration { get; set; }

    [Column("time", TypeName = "datetime")]
    public DateTime? Time { get; set; }

    [Column("total")]
    public double? Total { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("Orders")]
    public virtual Account? Account { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

    [ForeignKey("ServiceId")]
    [InverseProperty("Orders")]
    public virtual Service? Service { get; set; }
}
