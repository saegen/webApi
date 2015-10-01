using DataService.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DataService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDataService" in both code and config file together.
    [ServiceContract]
    public interface IDataService
    {
        //return url-friendly-name
        [OperationContract]
        ApiSubscription AddUserSubscription(int userId, ApiSubscription subscription);

        //return url-friendly-name
        [OperationContract]
        int AddUser(ApiUser user);

        [OperationContract]
        ApiUser GetUser(int userId);

        [OperationContract]
        IEnumerable<ApiUser> GetUsers();

        [OperationContract]
        void DeleteUser(int userId);

        [OperationContract]
        ApiUser UpdateUser(ApiUser user);
        
        [OperationContract]
        IEnumerable<ApiSubscription> GetUserSubscriptions(int userId);

        [OperationContract]
        IEnumerable<ApiSubscription> GetSubscriptions();

        [OperationContract]
        void DeleteSubscription(Guid subscriptionId);

        [OperationContract]
        ApiSubscription UpdateSubscription(ApiSubscription sub);
        
    }
}
