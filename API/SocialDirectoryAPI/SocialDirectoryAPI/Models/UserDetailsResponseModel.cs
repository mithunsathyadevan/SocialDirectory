using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialDirectoryAPI.Models
{
    public class UserDetailsResponseModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
    }
}
