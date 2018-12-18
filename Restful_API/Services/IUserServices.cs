using Restful_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restful_API.Services
{
    interface IUserServices
    {
        object CreateUser(UserModel input);
        object GetUser(GetUserModel input);
        object UpdateUser(UserModel input);
        object DeleteUser(LoginModel input);
        object Login(LoginModel input);
        object DeleteAll();
    }
}
