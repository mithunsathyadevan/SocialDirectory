using SocialDirectoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialDirectoryAPI.Contract
{
    public interface IMatchAlgo
    {
        Task<List<MatchResponse>> GetMatches(int userId);
    }
}
