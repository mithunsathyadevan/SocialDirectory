using Microsoft.AspNetCore.Http;
using SocailDirectoryModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialDirectoryAPI.Contract
{
    public interface IUserDetailsContract
    {
        UserModel GetUserDetails(HttpContext httpContext);
    }
}
