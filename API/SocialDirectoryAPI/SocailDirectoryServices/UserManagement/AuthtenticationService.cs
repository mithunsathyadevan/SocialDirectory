using Microsoft.EntityFrameworkCore;
using SocailDirectoryModels.Models;
using SocialDirectoryContracts.UserManagement;
using SocialDirectoryDataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SocailDirectoryServices.UserManagement
{
    public class AuthtenticationService : IAuthenticateContract
    {
        IJwtContract _jwtService;
        SocialDirectoryContext _context;
        public AuthtenticationService(IJwtContract jwtService,
            SocialDirectoryContext context)
        {
            _jwtService = jwtService;
            _context = context;
        }

        public string EncodePassword(string pass)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
        }
        public async Task<ResponseViewModel> RegisterUser(UserRegisterModel userRegisterModel)
        {
            ResponseViewModel retunModel = new ResponseViewModel();
            var userAlreadyExist = _context.UserDetails.Any(x => x.UserName == userRegisterModel.Email);
            if (userAlreadyExist)
            {
                retunModel.IsSuccess = false;
                retunModel.Message = "User Already Exist";
                return retunModel;
            }
            else
            {
                var userDetails = new UserDetail
                {
                    CreatedOn = DateTime.UtcNow,
                    Email = userRegisterModel.Email,
                    FirstName = userRegisterModel.FirstName,
                    LastName = userRegisterModel.LastName,
                    MiddleName = userRegisterModel.MiddleName,
                    UserName = userRegisterModel.Email
                };
                var location = _context.Locations.Where(x => x.LocationName.ToLower() == userRegisterModel.Location.ToLower()).First();
                if (location != null)
                {
                    userDetails.Location = location;
                }
                else
                {
                    Location locationRef = new Location
                    {
                        CreatedBy = null,
                        CreatedOn = DateTime.UtcNow,
                        LocationName = userRegisterModel.Location
                    };
                    userDetails.Location = locationRef;
                }
                retunModel.IsSuccess = true;
                retunModel.Message = "User Added Successfully";
                _context.UserDetails.Add(userDetails);
                var Id = await _context.SaveChangesAsync();
                var loginModel = new Login
                {
                    UserId = userDetails.UserId,
                    Password = EncodePassword(userRegisterModel.Password)
                };
                _context.Logins.Add(loginModel);
                await _context.SaveChangesAsync();
            }
            return retunModel;
        }
        public async Task<ResponseViewModel> Login(string username, string password)
        {
            ResponseViewModel response = new ResponseViewModel();
            var userDetails = await _context.UserDetails.Where(x => x.UserName == username).FirstOrDefaultAsync();
            if (userDetails != null)
            {
                var Login = await _context.Logins.Where(x => x.UserId == userDetails.UserId).FirstOrDefaultAsync();
                if (Login.Password == EncodePassword(password))
                {
                    response.IsSuccess = true;
                    UserRegisterModel model = new UserRegisterModel
                    {
                        UserId = userDetails.UserId,
                        Email = userDetails.Email,
                        FirstName = userDetails.FirstName,
                        LastName = userDetails.LastName
                    };
                    response.Result = _jwtService.GenerateSecurityToken(model);
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid Password";
                }
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid User name";
            }
            return response;
        }
        public async Task<UserDetailsModel> GetUserDetails(int userId)
        {
            UserDetailsModel response = new UserDetailsModel();
            var userDetails = await _context.UserDetails.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            if (userDetails != null)
            {
                response.Email = userDetails.Email;
                response.UserId = userDetails.UserId;
                response.Name = string.Format("{0} {1}", userDetails.FirstName, userDetails.LastName);
                if (userDetails.Location == null)
                {
                    var location = _context.Locations.Where(x => x.Id == userDetails.LocationId).FirstOrDefault();
                    if (location != null)
                        response.Location = location.LocationName;
                }
                else
                    response.Location = userDetails.Location.LocationName;
            }
            return response;
        }
    }
}
