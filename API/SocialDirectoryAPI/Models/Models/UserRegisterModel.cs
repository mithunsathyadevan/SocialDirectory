using System;
using System.Collections.Generic;
using System.Text;

namespace SocailDirectoryModels.Models
{
    public class UserRegisterModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string EncryptedPassword { get; set; }
        public int UserId { get; set; }
    }
}
