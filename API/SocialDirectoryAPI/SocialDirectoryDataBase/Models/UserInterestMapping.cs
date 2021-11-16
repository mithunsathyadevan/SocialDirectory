using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SocialDirectoryDataBase.Models
{
    [Table("UserInterestMapping", Schema = "dbo")]
    public partial class UserInterestMapping
    {
        [Key]
        public int Id { get; set; }
        public int? InterestId { get; set; }
        public int? UserId { get; set; }
        public bool Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        [ForeignKey(nameof(InterestId))]
        [InverseProperty("UserInterestMappings")]
        public virtual Interest Interest { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(UserDetail.UserInterestMappings))]
        public virtual UserDetail User { get; set; }
    }
}
