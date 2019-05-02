using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces
{
    [ServiceContract(Namespace = "http://dataservice/interfaces/user")]
    public interface IUserService
    {
        [OperationContract]
        ApiUser CreateUser(CreateUserDTO user);

        [OperationContract]
        void DeleteUser(int userId);

        [OperationContract]
        ApiUser GetUser(int userId);

        [OperationContract]
        IEnumerable<ApiUser> GetUsers();

        [OperationContract]
        ApiUser UpdateUser(UpdateUserDTO user);

    }
}
