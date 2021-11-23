using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocailDirectoryModels.Models;
using SocialDirectoryAPI.Models;
using SocialDirectoryContracts.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialDirectoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        IAuthenticateContract _authenticate;

        private readonly ILogger<AuthenticationController> _logger;
        public AuthenticationController
            (
            IAuthenticateContract authenticateContract,
            ILogger<AuthenticationController> logger
            )
        {
            _authenticate = authenticateContract;
            _logger = logger;
        }
        [HttpPost("RegisterUser")]
        public async Task<ResponseViewModel> RegisterUser(UserRegisterRequestModel requestModel)
        {

            var user = new UserRegisterModel
            {
                Email = requestModel.Email,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                MiddleName = requestModel.MiddleName,
                Password = requestModel.Password,
                UserName = requestModel.Email,
                Location=requestModel.Location
            };
            return await _authenticate.RegisterUser(user);
        }
        [HttpPost("Login")]
        public async Task<LoginResponseModel> Login(LoginModel loginModel)
        {
            LoginResponseModel returnData = new LoginResponseModel() ;

            var data = await _authenticate.Login(loginModel.UserName, loginModel.Password);
            if (data.IsSuccess == true)
            {
                returnData.IsSuccess = true;
                returnData.Token = data.Result;

            }
            else
            {
                returnData.IsSuccess = false;
                returnData.Token = data.Message;
            }
            return returnData;
        }
        [HttpGet("GetUserDetails")]
        [Authorize]
        public async Task<UserDetailsResponseModel> GetUserDetails()
        {
            UserDetailsResponseModel returnData = new UserDetailsResponseModel();
            int userId = Convert.ToInt32(HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var data = await _authenticate.GetUserDetails(userId);
            if(data!=null)
            {
                returnData.Email = data.Email;
                returnData.Name = data.Name;
                returnData.Location = data.Location;
                returnData.UserId = data.UserId;
            }
            
            return returnData;
        }

    }
}
