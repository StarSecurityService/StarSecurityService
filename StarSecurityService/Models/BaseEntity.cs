using System.ComponentModel.DataAnnotations.Schema;

namespace StarSecurityService.Models
{
    public class BaseEntity
    {
        [Column("CreatedAt")]
        public DateTime? CreatedDate { get; set; }
        [Column("UpdatedAt")]
        public DateTime? UpdatedDate { get; set; }
    }
}
