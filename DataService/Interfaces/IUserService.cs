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
        int CreateUser(ApiUser user);

        [OperationContract]
        ApiSubscription AddSubscriptions(int userId, IEnumerable<ApiSubscription> subscriptions);

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
