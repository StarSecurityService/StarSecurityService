using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StarSecurityService.Models;

[Table("Comment")]
public partial class Comment
{
    [Key]
    [Column("commentID")]
    public int CommentId { get; set; }

    [Column("feedID")]
    public int? FeedId { get; set; }

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

    [ForeignKey("FeedId")]
    [InverseProperty("Comments")]
    public virtual Feed? Feed { get; set; }
}
