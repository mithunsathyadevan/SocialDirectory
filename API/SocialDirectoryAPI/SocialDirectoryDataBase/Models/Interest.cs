using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SocialDirectoryDataBase.Models
{
    [Table("Interests", Schema = "dbo")]
    public partial class Interest
    {
        public Interest()
        {
            UserInterestMappings = new HashSet<UserInterestMapping>();
        }

        [Key]
        public int Id { get; set; }
        [Column("Interest Name")]
        [StringLength(50)]
        public string InterestName { get; set; }
        [Column("Interest Description")]
        [StringLength(200)]
        public string InterestDescription { get; set; }
        public int? ParentId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        [InverseProperty(nameof(UserInterestMapping.Interest))]
        public virtual ICollection<UserInterestMapping> UserInterestMappings { get; set; }
    }
}
