using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.UserService;
using Common;
using NLog;

namespace WebApi.Models
{
    public class UserRepo : IUserRepo
    {
        private Logger log = LogManager.GetCurrentClassLogger();
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
        public ApiUser CreateUser(CreateUserDTO user)
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
            ApiUser[] res = null;
            try
            {
                return _userClient.GetUsers();
            }
            catch (Exception e)
            {
                log.Error("UserRepo:GetUsers()" + e.Message );
            }
            return res;
        }

        public ApiUser UpdateUser(UpdateUserDTO user)
        {
            return _userClient.UpdateUser(user);
        }
    }
}