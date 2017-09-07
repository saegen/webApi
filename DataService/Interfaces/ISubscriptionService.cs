using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService.Types;
using System.ServiceModel;

namespace DataService.Interfaces
{
    [ServiceContract(Namespace = "http://dataservice/interfaces/subscription")]
    public interface ISubscriptionService
    {
        [OperationContract]
        ApiSubscription AddUserSubscription(int userId, ApiSubscription subscription);

        [OperationContract]
        IEnumerable<ApiSubscription> GetSubscriptions();
            
        [OperationContract]
        IEnumerable<ApiSubscription> GetUserSubscriptions(int userId);
   
        [OperationContract]
        void DeleteSubscription(Guid subscriptionId);

        [OperationContract]
        ApiSubscription UpdateSubscription(ApiSubscription subValues);
    }
}
