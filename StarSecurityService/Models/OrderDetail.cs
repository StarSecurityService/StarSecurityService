using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StarSecurityService.Models;

public partial class OrderDetail : BaseEntity
{
    [Key]
    [Column("orderdetailsID")]
    public int OrderdetailsId { get; set; }

    [Column("guardID")]
    public int? GuardId { get; set; }

    [Column("orderID")]
    public int? OrderId { get; set; }

    [ForeignKey("GuardId")]
    [InverseProperty("OrderDetails")]
    public virtual Guard? Guard { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderDetails")]
    public virtual Order? Order { get; set; }
}
