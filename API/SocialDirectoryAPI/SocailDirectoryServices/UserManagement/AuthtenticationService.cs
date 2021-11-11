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
        public AuthtenticationService(IJwtContract jwtService)
        {
            _jwtService = jwtService;
        }
        public void Get()
        {
            using (var context = new SocialDirectoryContext())
            {

                //var data = context.UserDetails.ToList();
                //return data.ToString();
            }

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
            var userDetails = new UserDetail
            {
                CreatedOn = DateTime.UtcNow,
                Email = userRegisterModel.Email,
                FirstName = userRegisterModel.FirstName,
                LastName = userRegisterModel.LastName,
                MiddleName = userRegisterModel.MiddleName,
                UserName = userRegisterModel.Email
            };
            using (var _context = new SocialDirectoryContext())
            {

                var userAlreadyExist = _context.UserDetails.Any(x => x.UserName == userRegisterModel.Email);
                if (userAlreadyExist)
                {
                    retunModel.IsSuccess = false;
                    retunModel.Message = "User Already Exist";
                    return retunModel;
                }
                else
                {
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
            }

            return retunModel;
        }
        public async Task<ResponseViewModel> Login(string username, string password)
        {
            ResponseViewModel response = new ResponseViewModel();
            using (var _context = new SocialDirectoryContext())
            {
                var userDetails =await _context.UserDetails.Where(x => x.UserName == username).FirstOrDefaultAsync();
                if(userDetails!=null)
                {

                    var Login = await _context.Logins.Where(x => x.UserId == userDetails.UserId).FirstOrDefaultAsync();
                   if(Login.Password== EncodePassword(password))
                    {
                        response.IsSuccess = true;
                        UserRegisterModel model = new UserRegisterModel
                        {
                            UserId=userDetails.UserId,
                            Email=userDetails.Email,
                            FirstName=userDetails.FirstName,
                            LastName=userDetails.LastName
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
            }
            return response;
        }
    }
}
