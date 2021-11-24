using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SocialDirectoryDataBase.Models
{
    [Table("UserDetails", Schema = "dbo")]
    public partial class UserDetail
    {
        public UserDetail()
        {
            ContactLists = new HashSet<ContactList>();
            Logins = new HashSet<Login>();
            UserInterestMappings = new HashSet<UserInterestMapping>();
        }

        [Key]
        public int UserId { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string MiddleName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public int? LocationId { get; set; }

        [ForeignKey(nameof(LocationId))]
        [InverseProperty("UserDetails")]
        public virtual Location Location { get; set; }
        [InverseProperty(nameof(ContactList.User))]
        public virtual ICollection<ContactList> ContactLists { get; set; }
        [InverseProperty(nameof(Login.User))]
        public virtual ICollection<Login> Logins { get; set; }
        [InverseProperty(nameof(UserInterestMapping.User))]
        public virtual ICollection<UserInterestMapping> UserInterestMappings { get; set; }
    }
}
