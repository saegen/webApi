using DataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Common;

namespace DataService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAdminService" in both code and config file together.
    [ServiceContract]
    public class AdminService : IAdminService
    {
        public void Subscribe(int userId, IEnumerable<ApiSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(int userId, Guid subscriptionId)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(int userId, IEnumerable<ApiSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeAll(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
