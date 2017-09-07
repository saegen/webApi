using DataService.Types;
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
        int AddUser(ApiUser user);

        [OperationContract]
        ApiSubscription AddUserSubscription(int userId, ApiSubscription subscription);

        [OperationContract]
        void DeleteUser(int userId);

        [OperationContract]
        ApiUser GetUser(int userId);

        [OperationContract]
        IEnumerable<ApiUser> GetUsers();

        [OperationContract]
        ApiUser UpdateUser(ApiUser user);

    }
}
