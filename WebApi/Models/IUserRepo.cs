using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UserServiceReference;

namespace WebApi.Models
{
    interface IUserRepo
    {
        ApiSubscription AddUserSubscription(int userId, ApiSubscription subscription);
        int AddUser(ApiUser user);
        ApiUser GetUser(int userId);
        IEnumerable<ApiUser> GetUsers();
        void DeleteUser(int userId);
        ApiUser UpdateUser(ApiUser user);
    }
}
