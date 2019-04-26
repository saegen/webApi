using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.ServiceModel;

namespace DataService.Interfaces
{
    [ServiceContract(Namespace = "http://dataservice/interfaces/admin")]
    public interface IAdminService
    {
        [OperationContract]
        void Subscribe(int userId, IEnumerable<ApiSubscription> subscriptions);

        [OperationContract]
        void Unsubscribe(int userId, Guid subscriptionId);

        [OperationContract]
        void UnsubscribeAll(int userId);

    }
}
