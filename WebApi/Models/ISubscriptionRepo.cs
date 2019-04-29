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
        //Hämta 1
        ApiSubscription GetSubscription(Guid subscriptionId);
        IEnumerable<ApiSubscription> GetSubscriptions();
        ApiSubscription CreateSubscription(ApiSubscription subscription);
        void DeleteSubscription(Guid subscriptionId);
        ApiSubscription UpdateSubscription(ApiSubscription sub);
    }
}
