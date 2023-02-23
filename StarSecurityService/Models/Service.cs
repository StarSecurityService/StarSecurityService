using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StarSecurityService.Models;

[Table("Service")]
public partial class Service
{
    [Key]
    [Column("serviceID")]
    public int ServiceId { get; set; }

    [Column("serviceName")]
    [StringLength(1)]
    public string? ServiceName { get; set; }

    [Column("description")]
    [StringLength(1)]
    public string? Description { get; set; }

    [Column("price")]
    public double? Price { get; set; }

    [InverseProperty("Service")]
    public virtual ICollection<Feedback> Feedbacks { get; } = new List<Feedback>();

    [InverseProperty("Service")]
    public virtual ICollection<Guard> Guards { get; } = new List<Guard>();

    [InverseProperty("Service")]
    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
