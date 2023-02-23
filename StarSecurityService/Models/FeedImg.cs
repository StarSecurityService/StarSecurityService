using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StarSecurityService.Models;

[Table("FeedImg")]
public partial class FeedImg
{
    [Key]
    [Column("feedimgID")]
    public int FeedimgId { get; set; }

    [Column("image")]
    [StringLength(255)]
    public string? Image { get; set; }

    [Column("feedID")]
    public int? FeedId { get; set; }

    [ForeignKey("FeedId")]
    [InverseProperty("FeedImgs")]
    public virtual Feed? Feed { get; set; }
}
