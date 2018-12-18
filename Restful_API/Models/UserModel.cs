using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restful_API.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public object Info { get; set; }
    }
}