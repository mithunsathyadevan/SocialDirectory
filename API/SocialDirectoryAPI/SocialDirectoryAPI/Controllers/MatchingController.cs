using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialDirectoryAPI.Models;
using SocialDirectoryContracts.Interest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialDirectoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchingController : ControllerBase
    {
        InterestContract _interestContract;
        public MatchingController(InterestContract interestContract)
        {
            _interestContract = interestContract;
        }
   
        
        [HttpGet("getMatches")]
        public async Task<List<MatchResponse>> GetSubInterest()
        {
            var returnData = new List<MatchResponse>();
            var data = await _interestContract.GetMatches();
            return data.Select(x => new MatchResponse
            {
                UserId = x.UserId,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).ToList();
        }
    }
}
