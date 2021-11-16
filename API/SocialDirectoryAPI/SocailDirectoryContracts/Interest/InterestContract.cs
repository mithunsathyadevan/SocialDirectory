using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SocailDirectoryModels.Models;
namespace SocialDirectoryContracts.Interest
{
    public interface InterestContract
    {
        Task<List<SocailDirectoryModels.Models.Interest>> GetInterests();
    }
}
