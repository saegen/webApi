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
        ApiUser CreateUser(CreateUserDTO user);
        ApiUser GetUser(int userId);
        IEnumerable<ApiUser> GetUsers();
        void DeleteUser(int userId);
        ApiUser UpdateUser(UpdateUserDTO user);
    }
}
