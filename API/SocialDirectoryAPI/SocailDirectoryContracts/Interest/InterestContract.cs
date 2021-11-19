using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SocailDirectoryModels.Models;
namespace SocialDirectoryContracts.Interest
{
    public interface InterestContract
    {
        Task<List<SocailDirectoryModels.Models.Interest>> GetMasterInterests(string search);
        Task<List<SocailDirectoryModels.Models.Interest>> GetSubInterests(int id);

        Task<List<MatchesModel>> GetMatches();
        Task<List<SocailDirectoryModels.Models.Interest>> GetUserInterest(int userId);
        Task<ResponseViewModel> DeleteInterest(int userId, int interestId);

        Task<ResponseViewModel> SaveInterest(int userId, int interestId);
        Task<List<SocailDirectoryModels.Models.Interest>> GetAllInterest();
    }
}
