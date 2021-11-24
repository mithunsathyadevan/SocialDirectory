using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SocialDirectoryDataBase.Models
{
    public class SP_MatchesModel
    {
        [Key]
        public int Id { get; set; }
   
        public int InterestId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
