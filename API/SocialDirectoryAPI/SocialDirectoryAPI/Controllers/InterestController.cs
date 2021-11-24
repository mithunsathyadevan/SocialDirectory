using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocailDirectoryModels.Models;
using SocialDirectoryAPI.Contract;
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
    public class InterestController : ControllerBase
    {
        InterestContract _interestContract;
        IUserDetailsContract _userDetails;
        public InterestController(InterestContract interestContract, IUserDetailsContract userDetails)
        {
            _interestContract = interestContract;
            _userDetails = userDetails;
        }
        [HttpGet("getinterests")]
        public async Task<List<MasterSearchDropDown>> GetMasterInterest(string search)
        {
            List<MasterSearchDropDown> returnData = new List<MasterSearchDropDown>();
            var data= await _interestContract.GetMasterInterests(search);
            var location = await _interestContract.GetLocations(search);
            returnData.AddRange( data.Select(x => new MasterSearchDropDown
            {
                Id=x.Id,
                Name=x.InterestName,
                Type= SocailDirectoryConstants.Interest,
                Selected=true
            }).ToList());
            returnData.AddRange(location.Select(x => new MasterSearchDropDown
            {
                Id=x.Item1,
                Name = x.Item2,
                Type = SocailDirectoryConstants.Location,
                Selected = true
            }).ToList());
            return returnData;
        }
        [HttpGet("getSubInterests")]
        public async Task<List<MasterSearchDropDown>> GetSubInterest(int id,string type)
        {
            var returnData = new List<MasterSearchDropDown>();
            if(type== "Interest")
            {
                var data = await _interestContract.GetSubInterests(id);
                return data.Select(x => new MasterSearchDropDown
                {
                    Id = x.Id,
                    Name = x.InterestName,
                    Type = SocailDirectoryConstants.Interest,
                    Selected = true
                }).ToList();
            }
            else
            {
                var data = await _interestContract.GetSubInterestsByLocation(id);
                return data.Select(x => new MasterSearchDropDown
                {
                    Id = x.Id,
                    Name = x.InterestName,
                    Type = SocailDirectoryConstants.Interest,
                    Selected = true
                }).ToList();
            }
         
        }
        [HttpGet("getAllInterest")]
        [Authorize]
        public async Task<List<MasterSearchDropDown>> GetAllInterest()
        {
            var returnData = new List<MasterSearchDropDown>();
            var data = await _interestContract.GetAllInterest();
            var user = _userDetails.GetUserDetails(HttpContext);
            var usersInterest = await _interestContract.GetUserInterest(user.UserId);
            var interest = usersInterest.Select(x => x.Id).ToArray();
            return data.Where(x=> !interest.Contains(x.Id)). Select(x => new MasterSearchDropDown
            {
                Id = x.Id,
                Name = x.InterestName,
                Type = SocailDirectoryConstants.Interest
            }).ToList();
        }
        [HttpGet("getUserInterests")]
        [Authorize]
        public async Task<List<InterestModelResponse>> GetUsersInterests()
        {
            var returnData = new List<InterestModelResponse>();
            var user = _userDetails.GetUserDetails(HttpContext);
            var data = await _interestContract.GetUserInterest(user.UserId);
            return data.Select(x => new InterestModelResponse
            {
                InterestId = x.Id,
                InterestName = x.InterestName
            }).ToList();
        }
        [HttpGet("Save")]
        [Authorize]
        public async Task<ResponseViewModel> Save(int interestId)
        {
            var returnData = new ResponseViewModel ();
            var user = _userDetails.GetUserDetails(HttpContext);
            var data = await _interestContract.SaveInterest(user.UserId,interestId);
            return returnData;
        
        }
        [HttpGet("DeleteUserInterest")]
        [Authorize]
        public async Task<ResponseViewModel> DeleteUserInterest(int interestId)
        {
            var returnData = new ResponseViewModel();
            var user = _userDetails.GetUserDetails(HttpContext);
            var data = await _interestContract.DeleteInterest(user.UserId, interestId);
            return returnData;
        }
    }
}
