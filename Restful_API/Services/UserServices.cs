using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Restful_API.Models;
using Newtonsoft.Json;

namespace Restful_API.Services
{
    public class UserServices : IUserServices
    {
        private List<UserModel> _listUser;
        private OutputModel _output;

        private string _filePath = HttpContext.Current.Request.MapPath("~/listUser.txt");

        public UserServices()
        {
            _listUser = new List<UserModel>();
            _output = new OutputModel();
        }

        private void ReadFileListUser()
        {
            try
            {
                using (StreamReader sr = new StreamReader(_filePath))
                {
                    string json = sr.ReadToEnd();
                    _listUser = JsonConvert.DeserializeObject<List<UserModel>>(json);
                    if (_listUser == null)
                        _listUser = new List<UserModel>();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void WriteFileListUser()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_filePath))
                {
                    sw.Write(JsonConvert.SerializeObject(_listUser).ToString());
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public object DeleteAll()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_filePath))
                {
                    _listUser = new List<UserModel>();
                    sw.Write(JsonConvert.SerializeObject(_listUser).ToString());
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void SetOutput(bool error, int errorCode, string message, object result)
        {
            _output.Error = error;
            _output.ErrorCode = errorCode;
            _output.Message = message;
            _output.Result = result;
        }

        public object CreateUser(UserModel input)
        {
            ReadFileListUser();
            if (_listUser == null)
                _listUser = new List<UserModel>();
            try
            {
                foreach (var user in _listUser)
                    if (user.UserName == input.UserName)
                    {
                            SetOutput(
                                error: true,
                                errorCode: 403,
                                message: "username is valid",
                                result: ""
                            );
                        return _output;
                    }
                _listUser.Add(input);
                SetOutput(
                    error: false,
                    errorCode: 200,
                    message: "",
                    result: input
                    );
                WriteFileListUser();
            }
            catch (Exception ex)
            {
                SetOutput(
                    error: true,
                    errorCode: ex.GetHashCode(),
                    message: ex.Message,
                    result: null
                    );
            }
            return _output;
        }

        public object DeleteUser(LoginModel input)
        {
            ReadFileListUser();
            bool isUser = false;
            foreach(var user in _listUser)
                if (user.UserName == input.UserName && user.Password == input.Password)
                {
                    _listUser.Remove(user);
                    isUser = true;
                    break;
                }
            if (isUser)
            {
                SetOutput(
                    error: false,
                    errorCode: 200,
                    message: "",
                    result: "OK"
                    );
                WriteFileListUser();
            }
            else
                SetOutput(
                    error: true,
                    errorCode: 404,
                    message: "Not found User with username and password",
                    result: null);
            return _output;
        }

        public object GetUser(GetUserModel input)
        {
            ReadFileListUser();
            if (input == null)
            {
                List<object> userInfo = new List<object>();
                foreach (var u in _listUser)
                    userInfo.Add(u.Info);
                SetOutput(
                    error: false,
                    errorCode: 200,
                    message: "",
                    result: userInfo
                    );
                return _output;
            }
            UserModel user = null;
            foreach (var u in _listUser)
                if (u.UserName == input.UserName)
                {
                    user = u;
                    break;
                }
            if (user != null)
                SetOutput(
                    error: false,
                    errorCode: 200,
                    message: "",
                    result: user.Info
                    );
            else
                SetOutput(
                    error: true,
                    errorCode: 404,
                    message: "Not found User",
                    result: null);
            return _output;
        }

        public object Login(LoginModel input)
        {
            ReadFileListUser();
            UserModel user = null;
            foreach (var u in _listUser)
                if (u.UserName == input.UserName && u.Password == input.Password)
                {
                    user = u;
                    break;
                }
            if (user != null)
                SetOutput(
                    error: false,
                    errorCode: 200,
                    message: "",
                    result: "Login success"
                    );
            else
                SetOutput(
                    error: true,
                    errorCode: 404,
                    message: "Username or Password is invalid",
                    result: "");
            return _output;
        }

        public object UpdateUser(UserModel input)
        {
            ReadFileListUser();
            var isUser = false;
            foreach (var u in _listUser)
                if (u.UserName == input.UserName)
                {
                    u.Info = input.Info;
                    isUser = true;
                    break;
                }
            if (isUser)
            {
                SetOutput(
                    error: false,
                    errorCode: 200,
                    message: "",
                    result: input.Info
                    );
                WriteFileListUser();
            }
            else
                SetOutput(
                    error: true,
                    errorCode: 404,
                    message: "Not found User",
                    result: "");
            return _output;
        }
    }
}