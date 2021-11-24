using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocailDirectoryModels.Models;
using SocialDirectoryAPI.Contract;
using SocialDirectoryAPI.Models;
using SocialDirectoryContracts.Contact;
using SocialDirectoryContracts.Interest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialDirectoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MatchingController : ControllerBase
    {
        InterestContract _interestContract;
        IMatchAlgo _matchAlgoService;
        IContacts _contacts;
        IUserDetailsContract _userDetails;
        public MatchingController
            (
            InterestContract interestContract,
            IContacts contacts,
            IUserDetailsContract userDetails,
            IMatchAlgo matchAlgo
            )
        {
            _interestContract = interestContract;
            _contacts = contacts;
            _userDetails = userDetails;
            _matchAlgoService = matchAlgo;
        }

        [HttpGet("getMatches")]
        public async Task<List<MatchResponse>> GetMatches()
        {

            var user = _userDetails.GetUserDetails(HttpContext);
            return await _matchAlgoService.GetMatches(user.UserId);
        }

        [HttpPost("ListInterest")]
        public async Task<List<MatchResponse>> GetMatchesBySearchFilter(InterestRequest interest)
        {
            var user = _userDetails.GetUserDetails(HttpContext);
            var returnData = new List<MatchResponse>();
            var inter = interest.InterestIds.Select(x => x).ToArray();
            var data = await _interestContract.GetMatches(inter, user.UserId,interest.LocationId);
            var contacts = await _contacts.GetContacts(user.UserId);
            var contactIds = contacts.Select(x => x.UserId).ToArray();
            var dataModel = data.Where(x => !contactIds.Contains(x.UserId)).ToList();
            return dataModel.Select(x => new MatchResponse
            {
                UserId = x.UserId,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).ToList();
        }
    }
}
