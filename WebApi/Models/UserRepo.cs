using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.UserService;
using Common;

namespace WebApi.Models
{
    public class UserRepo : IUserRepo
    {
        private UserServiceClient _userClient;

        public UserRepo(IUserService userServiceClient)
        {
            _userClient = userServiceClient as UserServiceClient;
        }
        ~UserRepo()
        {
            if (_userClient != null)
            {
                try
                {
                    _userClient.Close();
                }
                catch (Exception)
                {
                    _userClient.Abort();
                }                
            }
        }
        public ApiUser CreateUser(ApiUser user)
        {
            return _userClient.CreateUser(user);
        }

        public void DeleteUser(int userId)
        {
            _userClient.DeleteUser(userId);
        }

        public ApiUser GetUser(int userId)
        {
            return _userClient.GetUser(userId);
        }

        public IEnumerable<ApiUser> GetUsers()
        {
            return _userClient.GetUsers();
        }

        public ApiUser UpdateUser(ApiUser user)
        {
            return _userClient.UpdateUser(user);
        }
    }
}