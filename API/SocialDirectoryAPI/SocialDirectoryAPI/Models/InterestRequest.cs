using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialDirectoryAPI.Models
{
    public class InterestRequest
    {
        public InterestRequest()
        {
            InterestIds = new List<int>();
        }
        public List<int> InterestIds { get; set; }
        public Nullable<int> LocationId { get; set; }
    }

}
