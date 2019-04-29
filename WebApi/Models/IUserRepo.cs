using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace WebApi.Models
{
    interface IUserRepo
    {
        ApiUser CreateUser(ApiUser user);
        ApiUser GetUser(int userId);
        IEnumerable<ApiUser> GetUsers();
        void DeleteUser(int userId);
        ApiUser UpdateUser(ApiUser user);
    }
}
