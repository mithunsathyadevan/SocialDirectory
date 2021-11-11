using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialDirectoryAPI.Models
{
    public class LoginResponseModel
    {
        public string Token { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
