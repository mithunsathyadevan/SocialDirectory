using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SocialDirectoryDataBase.Models
{
    [Table("Login", Schema = "dbo")]
    public partial class Login
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        [StringLength(300)]
        public string Password { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(UserDetail.Logins))]
        public virtual UserDetail User { get; set; }
    }
}
