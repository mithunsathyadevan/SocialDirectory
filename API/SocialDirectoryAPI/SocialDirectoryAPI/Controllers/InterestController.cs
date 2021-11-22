using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocailDirectoryModels.Models;
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
        public InterestController(InterestContract interestContract)
        {
            _interestContract = interestContract;
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
                Type="Interest",
                Selected=true
            }).ToList());
            returnData.AddRange(location.Select(x => new MasterSearchDropDown
            {
                Id=x.Item1,
                Name = x.Item2,
                Type = "Location",
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
                    Type = "Interest",
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
                    Type = "Interest",
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
            int userId = Convert.ToInt32(HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var usersInterest = await _interestContract.GetUserInterest(userId);
            var interest = usersInterest.Select(x => x.Id).ToArray();
            return data.Where(x=> !interest.Contains(x.Id)). Select(x => new MasterSearchDropDown
            {
                Id = x.Id,
                Name = x.InterestName,
                Type = "Interest"
            }).ToList();
        }
        [HttpGet("getUserInterests")]
        [Authorize]
        public async Task<List<InterestModelResponse>> GetUsersInterests()
        {
            var returnData = new List<InterestModelResponse>();

            int userId = Convert.ToInt32(HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var data = await _interestContract.GetUserInterest(userId);
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

            int userId = Convert.ToInt32(HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var data = await _interestContract.SaveInterest(userId,interestId);
            return returnData;
        
        }
        [HttpGet("DeleteUserInterest")]
        [Authorize]
        public async Task<ResponseViewModel> DeleteUserInterest(int interestId)
        {
            var returnData = new ResponseViewModel();

            int userId = Convert.ToInt32(HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var data = await _interestContract.DeleteInterest(userId, interestId);
            return returnData;

        }
    }
}
