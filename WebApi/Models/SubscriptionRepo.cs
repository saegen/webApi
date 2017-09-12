using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.SubscriptionServiceReference;
using Common;

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
            return _client.AddUserSubscription(userId, subscription);
        }

        public void DeleteSubscription(Guid subscriptionId)
        {
            _client.DeleteSubscription(subscriptionId);
        }

        public IEnumerable<ApiSubscription> GetSubscriptions()
        {
            return _client.GetSubscriptions();
        }

        public IEnumerable<ApiSubscription> GetUserSubscriptions(int userId)
        {
            return _client.GetUserSubscriptions(userId);
        }

        public ApiSubscription UpdateSubscription(ApiSubscription sub)
        {
            return _client.UpdateSubscription(sub);
        }
    }
}