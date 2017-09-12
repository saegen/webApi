using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace WebApi.Models
{
    interface ISubscriptionRepo
    {
        ApiSubscription AddUserSubscription(int userId, ApiSubscription subscription);
        IEnumerable<ApiSubscription> GetUserSubscriptions(int userId);
        IEnumerable<ApiSubscription> GetSubscriptions();
        void DeleteSubscription(Guid subscriptionId);
        ApiSubscription UpdateSubscription(ApiSubscription sub);
    }
}
