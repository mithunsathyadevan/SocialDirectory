using System;
using System.Collections.Generic;

#nullable disable

namespace SocialDirectoryDataBase.Models
{
    public partial class UserDetail
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
