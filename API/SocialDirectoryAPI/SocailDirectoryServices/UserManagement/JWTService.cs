using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocailDirectoryModels.Models;
using SocialDirectoryContracts.UserManagement;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SocailDirectoryServices.UserManagement
{
   public class JWTService : IJwtContract
    {
        private readonly string _secret;
        private readonly string _expDate;

        public JWTService(IConfiguration config)
        {
            _secret = config.GetSection("JwtConfig").GetSection("secret").Value;
            _expDate = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
        }
        private byte[] Encode(string pass)
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
            return encodedBytes;
        }
        public string GenerateSecurityToken(UserRegisterModel model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encode(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(ClaimTypes.Name,string.Format("{0} {1}",model.FirstName,model.LastName)),
                    new Claim("UserId", model.UserId.ToString ())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
