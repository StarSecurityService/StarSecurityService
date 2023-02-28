using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StarSecurityService.Models;

[Table("Service")]
public partial class Service : BaseEntity
{
    [Key]
    [Column("serviceID")]
    public int ServiceId { get; set; }

    [Column("serviceName")]
    [StringLength(255)]
    public string? ServiceName { get; set; }

    [Column("description")]
    [StringLength(1000)]
    public string? Description { get; set; }

    [Column("price")]
    public double? Price { get; set; }

    [Column("image")]
    public string? Image { get; set; }

    [InverseProperty("Service")]
    public virtual ICollection<Feedback> Feedbacks { get; } = new List<Feedback>();

    [InverseProperty("Service")]
    public virtual ICollection<Guard> Guards { get; } = new List<Guard>();

    [InverseProperty("Service")]
    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
