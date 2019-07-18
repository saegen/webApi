using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    interface IAdminRepo
    {
        IEnumerable<ApiSubscription> GetUserSubscriptions(int userId);
        IEnumerable<ApiUser> GetSubscriptionUsers(Guid subscriptionId);
        void Unsubscribe(int userId, Guid subscriptionId);
        void Subscribe(int userId, Guid[] subscriptionIds);
    }
}
