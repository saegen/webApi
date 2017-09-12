using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.UserServiceReference;
using Common;

namespace WebApi.Models
{
    public class UserRepo : IUserRepo
    {
        private UserServiceClient _userClient;

        public ApiSubscription AddUserSubscription(int userId, ApiSubscription subscription)
        {
            throw new NotImplementedException();
        }

        public UserRepo(UserServiceClient userServiceClient)
        {
            _userClient = userServiceClient;
        }

        public int AddUser(ApiUser user)
        {
            return _userClient.AddUser(user);
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