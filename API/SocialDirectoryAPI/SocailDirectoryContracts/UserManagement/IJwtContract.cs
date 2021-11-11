using SocailDirectoryModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialDirectoryContracts.UserManagement
{
    public interface IJwtContract
    {
        string GenerateSecurityToken(UserRegisterModel model);
    }
}
