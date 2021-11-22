using SocialDirectoryContracts.Interest;
using SocialDirectoryDataBase.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SocailDirectoryModels.Models;

namespace SocailDirectoryServices.Interest
{
    public class InterestService : InterestContract
    {
        SocialDirectoryContext _context;
        public InterestService(
            SocialDirectoryContext context)
        {
            _context = context;
        }
        public async Task<List<SocailDirectoryModels.Models.Interest>> GetMasterInterests(string search)
        {
            List<SocailDirectoryModels.Models.Interest> returnData = new List<SocailDirectoryModels.Models.Interest>();
            returnData = await _context.Interests.Where(x => x.ParentId == null && x.InterestName.Contains(search)).Take(10).Select(x => new SocailDirectoryModels.Models.Interest
            {
                Id = x.Id,
                InterestName = x.InterestName
            }).ToListAsync();
            return returnData;
        }

        public async Task<List<string>> GetLocations(string search)
        {
            var dataSource = await _context.UserDetails.AsNoTracking().Where(x => x.LocationName.Contains(search))
                .Select(x => x.LocationName).ToListAsync();
            return dataSource;


        }

        public async Task<List<SocailDirectoryModels.Models.Interest>> GetSubInterests(int id)
        {
            List<SocailDirectoryModels.Models.Interest> returnData = new List<SocailDirectoryModels.Models.Interest>();
            returnData = await _context.Interests.Where(x => x.ParentId.Value == id).Select(x => new SocailDirectoryModels.Models.Interest
            {
                Id = x.Id,
                InterestName = x.InterestName
            }).ToListAsync();
            return returnData;
        }
        public async Task<List<SocailDirectoryModels.Models.Interest>> GetAllInterest()
        {
            List<SocailDirectoryModels.Models.Interest> returnData = new List<SocailDirectoryModels.Models.Interest>();
            returnData = await _context.Interests.Where(x => x.ParentId != null).Select(x => new SocailDirectoryModels.Models.Interest
            {
                Id = x.Id,
                InterestName = x.InterestName
            }).ToListAsync();
            return returnData;
        }
        public async Task<List<SocailDirectoryModels.Models.Interest>> GetInterest()
        {
            List<SocailDirectoryModels.Models.Interest> returnData = new List<SocailDirectoryModels.Models.Interest>();
            returnData = await _context.Interests.Select(x => new SocailDirectoryModels.Models.Interest
            {
                Id = x.Id,
                InterestName = x.InterestName
            }).OrderBy(x => x.Id).ToListAsync();
            return returnData;
        }
        public async Task<List<SocailDirectoryModels.Models.Interest>> GetUserInterest(int userId)
        {
            var returnData = from map in _context.UserInterestMappings
                             join interetst in _context.Interests on map.InterestId
                                equals interetst.Id
                             where map.UserId == userId && interetst.ParentId != null
                             select new SocailDirectoryModels.Models.Interest
                             {
                                 Id = interetst.Id,
                                 InterestName = interetst.InterestName
                             };
            return await returnData.ToListAsync();
        }
        public async Task<List<MatchesModel>> GetMatches()
        {
            List<MatchesModel> returnData = new List<MatchesModel>();
            var data = from user in _context.UserDetails
                       join map in _context.UserInterestMappings on
user.UserId equals map.UserId
                       join interest in _context.Interests on
map.InterestId equals interest.Id
                       select new MatchesModel
                       {
                           UserId = user.UserId,
                           FirstName = user.FirstName,
                           LastName = user.LastName,
                           Email = user.Email,
                           InterestId= interest.Id

                       };
            returnData = await data.ToListAsync();

            return returnData;
        }
        public async Task<List<MatchesModel>> GetMatches(int[] interests, int userId)
        {

            var returnData = from map in _context.UserInterestMappings
                             join interest in _context.Interests
on map.InterestId equals interest.Id
                             join user in _context.UserDetails on map.UserId
equals user.UserId
                             where user.UserId != userId && interests.Contains(interest.Id)
                             select new MatchesModel
                             {
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 UserId = user.UserId,
                                 Email = user.Email
                             };

            return await returnData.Distinct().ToListAsync();
        }
        public async Task<ResponseViewModel> DeleteInterest(int userId, int interestId)
        {
            ResponseViewModel returnData = new ResponseViewModel();

            var dataSource = _context.UserInterestMappings.Where(x => x.InterestId == interestId && x.UserId == userId).FirstOrDefault();
            if (dataSource != null)
            {
                _context.UserInterestMappings.Remove(dataSource);
                await _context.SaveChangesAsync();
                returnData.IsSuccess = true;
                returnData.Message = "Successfully deleted record";
            }
            else
            {
                returnData.IsSuccess = false;
                returnData.Message = "Failed to delete record";
            }

            return returnData;
        }
        public async Task<ResponseViewModel> SaveInterest(int userId, int interestId)
        {
            ResponseViewModel returnData = new ResponseViewModel();
            UserInterestMapping model = new UserInterestMapping
            {
                InterestId = interestId,
                Active = true,
                CreatedBy = userId.ToString(),
                UserId = userId,
                CreatedOn = DateTime.UtcNow
            };
            if (_context.UserInterestMappings.Any(x => x.UserId == userId && x.InterestId == interestId))
            {
                returnData.IsSuccess = false;
                returnData.Message = "Interest Already Added";
            }
            else
            {
                var interesst = _context.Interests.Where(x => x.Id == interestId).FirstOrDefault();
                if (interesst != null)
                {
                    if (!_context.UserInterestMappings.Any(x => x.UserId == userId && x.InterestId == interesst.ParentId) && interesst.ParentId != null)
                    {
                        UserInterestMapping parentModel = new UserInterestMapping
                        {
                            InterestId = interesst.ParentId,
                            Active = true,
                            CreatedBy = userId.ToString(),
                            UserId = userId,
                            CreatedOn = DateTime.UtcNow
                        };
                        await _context.UserInterestMappings.AddAsync(parentModel);
                    }
                }
                returnData.IsSuccess = true;
                await _context.UserInterestMappings.AddAsync(model);
                await _context.SaveChangesAsync();
            }


            return returnData;
        }
    }
}
