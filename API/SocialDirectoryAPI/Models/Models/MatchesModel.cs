using System;
using System.Collections.Generic;
using System.Text;

namespace SocailDirectoryModels.Models
{
    public class MatchesModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int InterestId { get; set; }
       
    }
    public class MatchesModelGroup
    {
        public MatchesModelGroup()
        {
            InterstList = new List<MatchesModel>();
        }
        public int UserId { get; set; }
        public string BinaryVal { get; set; }
        public decimal Rank { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }
        public List<MatchesModel> InterstList { get; set; }



    }
}
