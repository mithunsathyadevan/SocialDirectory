using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SocialDirectoryDataBase.Models
{
    [Table("ContactList", Schema = "dbo")]
    public partial class ContactList
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ContactId { get; set; }
        public bool Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(UserDetail.ContactLists))]
        public virtual UserDetail User { get; set; }
    }
}
