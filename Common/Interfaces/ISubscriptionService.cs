using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DataService.Types;
using Common;
using System.ServiceModel;

namespace DataService.Interfaces
{
    [ServiceContract(Namespace = "http://dataservice/interfaces/subscription")]
    public interface ISubscriptionService
    {
        [OperationContract]
        ApiSubscription GetSubscription(Guid subscriptionId);

        [OperationContract]
        IEnumerable<ApiSubscription> GetSubscriptions();
            
        [OperationContract]
        void DeleteSubscription(Guid subscriptionId);

        [OperationContract]
        ApiSubscription UpdateSubscription(ApiSubscription subValues);

        [OperationContract]
        ApiSubscription CreateSubscription(ApiSubscription subValues);
    }
}
