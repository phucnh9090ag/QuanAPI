using Restful_API.Models;
using Restful_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Restful_API.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserServices _userServices; 
        public UserController()
        {
            _userServices = new UserServices();
        }

        [Route("api/User")]
        [HttpGet]
        public IHttpActionResult GetUser([FromUri]GetUserModel input)
        {
            var result = _userServices.GetUser(input);
            return Ok(result);
        }

        [Route("api/SignUp")]
        [HttpPost]
        public IHttpActionResult CreateUser([FromBody]UserModel input)
        {
            var result = _userServices.CreateUser(input);
            return Ok(result);
        }

        [Route("api/User")]
        [HttpPut]
        public IHttpActionResult UpdateUser([FromBody]UserModel input)
        {
            var result = _userServices.UpdateUser(input);
            return Ok(result);
        }

        [Route("api/User")]
        [HttpDelete]
        public IHttpActionResult DeleteUser([FromBody]LoginModel input)
        {
            var result = _userServices.DeleteUser(input);
            return Ok(result);
        }

        [Route("api/Login")]
        [HttpPost]
        public IHttpActionResult Login([FromBody]LoginModel input)
        {
            var result = _userServices.Login(input);
            return Ok(result);
        }

        [Route("api/Delete")]
        [HttpDelete]
        public IHttpActionResult DeleteAll()
        {
            var result = _userServices.DeleteAll();
            return Ok(result);
        }
    }
}
