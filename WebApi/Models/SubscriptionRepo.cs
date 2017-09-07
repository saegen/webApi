using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.SubscriptionServiceReference;

namespace WebApi.Models
{
    public class SubscriptionRepo : ISubscriptionRepo
    {
        private SubscriptionServiceClient _client;

        public SubscriptionRepo(SubscriptionServiceClient client)
        {
            _client = client;
        }

        public ApiSubscription AddUserSubscription(int userId, ApiSubscription subscription)
        {
            throw new NotImplementedException();
        }

        public void DeleteSubscription(Guid subscriptionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApiSubscription> GetSubscriptions()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApiSubscription> GetUserSubscriptions(int userId)
        {
            throw new NotImplementedException();
        }

        public ApiSubscription UpdateSubscription(ApiSubscription sub)
        {
            throw new NotImplementedException();
        }
    }
}