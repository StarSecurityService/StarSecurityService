using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StarSecurityService.Models;

[Table("Feedback")]
public partial class Feedback : BaseEntity
{
    [Key]
    [Column("feedbackID")]
    public int FeedbackId { get; set; }

    [Column("serviceID")]
    public int? ServiceId { get; set; }

    [Column("email")]
    [StringLength(255)]
    public string? Email { get; set; }

    [Column("firstName")]
    [StringLength(255)]
    public string? FirstName { get; set; }

    [Column("lastName")]
    [StringLength(255)]
    public string? LastName { get; set; }

    [Column("content")]
    [StringLength(255)]
    public string? Content { get; set; }

    [Column("postTime", TypeName = "datetime")]
    public DateTime? PostTime { get; set; }

    [ForeignKey("ServiceId")]
    [InverseProperty("Feedbacks")]
    public virtual Service? Service { get; set; }
}
