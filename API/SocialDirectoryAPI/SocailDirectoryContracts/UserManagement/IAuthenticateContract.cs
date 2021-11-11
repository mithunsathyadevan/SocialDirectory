using SocailDirectoryModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialDirectoryContracts.UserManagement
{
   public interface IAuthenticateContract
    {
        Task<ResponseViewModel> RegisterUser(UserRegisterModel userRegisterModel);
        string EncodePassword(string pass);
        Task<ResponseViewModel> Login(string username, string password);
    }
}
