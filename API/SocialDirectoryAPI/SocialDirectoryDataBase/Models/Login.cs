using System;
using System.Collections.Generic;

#nullable disable

namespace SocialDirectoryDataBase.Models
{
    public partial class Login
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Password { get; set; }
    }
}
