using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SocialDirectoryDataBase.Models
{
    [Table("Location", Schema = "dbo")]
    public partial class Location
    {
        public Location()
        {
            UserDetails = new HashSet<UserDetail>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string LocationName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }

        [InverseProperty(nameof(UserDetail.Location))]
        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
