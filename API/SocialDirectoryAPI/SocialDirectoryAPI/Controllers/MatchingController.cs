using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocailDirectoryModels.Models;
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
        IContacts _contacts;
        public MatchingController(InterestContract interestContract, IContacts contacts)
        {
            _interestContract = interestContract;
            _contacts = contacts;
        }


        [HttpGet("getMatches")]
        public async Task<List<MatchResponse>> GetMatches()
        {

            int userId = Convert.ToInt32(HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            
            var data = await _interestContract.GetMatches();
            var usersInterest = data.GroupBy(x =>new { x.UserId ,x.FirstName,x.LastName,x.Email}, (key, g) =>new MatchesModelGroup { 
            InterstList= g.ToList(),
            UserId= key.UserId,
            Email=key.Email,
            LastName=key.LastName,
            FirstName=key.FirstName,
            
            
            }  ).ToList();
            var intersts = await _interestContract.GetInterest();
            var length = Convert.ToInt32(Math.Pow(2, intersts.Count()));
            int[] dataSet = new int[length];
            Array.Clear(dataSet, 0, dataSet.Length);
            #region DataSetBuild
            foreach (var interest in usersInterest)
            {
                StringBuilder str = new StringBuilder();
                for (int i = 0; i < intersts.Count; i++)
                {
                    if (interest.InterstList.Any(x => x.InterestId == intersts[i].Id))
                    {
                        str.Append(1);
                    }
                    else
                    {
                        str.Append(0);
                    }
                }


                int index = Convert.ToInt32(str.ToString(), 2);
               
                interest.BinaryVal = str.ToString();
                dataSet[index]++;

            }
            #endregion
            var targetString = usersInterest.Where(x => x.UserId == userId).First().BinaryVal;
            foreach (var interest in usersInterest)
            {
                decimal rank = 0;
                for (int i = 0; i < interest.BinaryVal.Count(); i++)
                {
                    if (interest.BinaryVal[i] == targetString[i])
                    {
                        rank++;
                    }
                    else
                    {
                        StringBuilder target1 =new StringBuilder(interest.BinaryVal);
                        StringBuilder target2 = new StringBuilder(interest.BinaryVal);
                        target1[i] ='1';
                        target2[i] = '0';
                        var total = dataSet[Convert.ToInt32(target1.ToString(), 2)] + dataSet[Convert.ToInt32(target2.ToString(), 2)];
                        rank = (decimal)rank+(decimal)dataSet[Convert.ToInt32(target1.ToString(), 2)] / total;
                    }
                }
                interest.Rank = rank;

            }
            var contacts = await _contacts.GetContacts(userId);
            var contactIds = contacts.Select(x => x.UserId).ToArray();
            return usersInterest.OrderByDescending(x=>x.Rank).Where(x=>!contactIds.Contains(x.UserId)). Select(x => new MatchResponse
            {
                UserId =x.UserId,
                FirstName = x.FirstName,
                LastName = x.LastName,

                
            }).Where(x=>x.UserId!=userId).ToList();
        }
        [HttpPost("ListInterest")]

        public async Task<List<MatchResponse>> GetMatches(InterestRequest interest)
        {
            int userId = Convert.ToInt32(HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var returnData = new List<MatchResponse>();
            var inter = interest.InterestIds.Select(x => x).ToArray();
            var data = await _interestContract.GetMatches(inter, userId,interest.LocationId);
            var contacts = await _contacts.GetContacts(userId);
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
