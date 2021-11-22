using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialDirectoryAPI.Models
{
    public class MatchResponse
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public double Rank { get; set; }

        public string Name { get {
                return string.Format("{0} {1}", FirstName, LastName);
            
            } }
        public string ShortName
        {
            get
            {
                return string.Format("{0}{1}", FirstName[0], LastName[0]);

            }
        }
    }
}
