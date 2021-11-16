using SocialDirectoryContracts.Interest;
using SocialDirectoryDataBase.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
        public async Task<List<SocailDirectoryModels.Models.Interest>> GetInterests()
        {
            List<SocailDirectoryModels.Models.Interest> returnData = new List<SocailDirectoryModels.Models.Interest>();
            returnData =await _context.Interests.Select(x => new SocailDirectoryModels.Models.Interest
            {
                Id=x.Id,
                InterestName=x.InterestName
            }).ToListAsync();
            return returnData;
        }
    }
}
