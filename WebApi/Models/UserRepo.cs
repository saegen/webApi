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
            try
            {
                _userClient.DeleteUser(userId);
            }
            catch (Exception ex)
            {
                log.Error("UserRepo: DeleteUser: " + userId + ": " + ex.Message);
                throw;
            }
        }

        public ApiUser GetUser(int userId)
        {
            ApiUser user;
            try
            {
                user = _userClient.GetUser(userId);
            }
            catch (Exception ex)
            {
                log.Error("UserRepo: GetUser: " + userId + ": " + ex.Message);
                throw;
            }
            return user;
            
        }

        public IEnumerable<ApiUser> GetUsers()
        {
            ApiUser[] res = null;
            try
            {
                res = _userClient.GetUsers();
            }
            catch (Exception e)
            {
                log.Error("UserRepo:GetUsers()" + e.Message );
                throw;
            }
            return res;
        }

        public ApiUser UpdateUser(UpdateUserDTO user)
        {
            ApiUser updUser = null;
            try
            {
                updUser = _userClient.UpdateUser(user);
            }
            catch (Exception ex)
            {
                log.Error("UserRepo: UpdateUser: " + user.Id + " : " + ex.Message);
                throw;
            }
            return updUser;
        }
    }
}