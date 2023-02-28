using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StarSecurityService.Models;

public partial class Feed : BaseEntity
{
    [Key]
    [Column("feedID")]
    public int FeedId { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string? Title { get; set; }

    [Column("content")]
    [StringLength(255)]
    public string? Content { get; set; }

    [Column("tag")]
    [StringLength(255)]
    public string? Tag { get; set; }

    [Column("postTime", TypeName = "datetime")]
    public DateTime? PostTime { get; set; }

    [InverseProperty("Feed")]
    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    [InverseProperty("Feed")]
    public virtual ICollection<FeedImg> FeedImgs { get; } = new List<FeedImg>();
}
