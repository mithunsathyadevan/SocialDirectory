using Microsoft.AspNetCore.Http;
using SocailDirectoryModels.Models;
using SocialDirectoryAPI.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialDirectoryAPI.Services
{
    public class UserDetailsService : IUserDetailsContract
    {
        public UserModel GetUserDetails(HttpContext httpContext)
        {
            UserModel user = new UserModel();
            user.UserId= Convert.ToInt32(httpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            return user;
        }
    }
}
