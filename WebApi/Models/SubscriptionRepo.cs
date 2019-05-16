using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using WebApi.SubscriptionService;

namespace WebApi.Models
{
    public class SubscriptionRepo : ISubscriptionRepo
    {
        private SubscriptionServiceClient _client;

        public SubscriptionRepo(SubscriptionServiceClient client)
        {
            _client = client;
        }
        ~SubscriptionRepo()
        {
            if (_client != null)
            {
                try
                {
                    _client.Close();
                }
                catch (Exception)
                {
                    _client.Abort();
                }
                
            }
        }


        public ApiSubscription CreateSubscription(ApiSubscription subscription)
        {

            return _client.CreateSubscription(subscription);
        }

        public void DeleteSubscription(Guid subscriptionId)
        {
            _client.DeleteSubscription(subscriptionId);
        }

        public ApiSubscription GetSubscription(Guid subscriptionId)
        {
            return _client.GetSubscription(subscriptionId); 
        }

        public IEnumerable<ApiSubscription> GetSubscriptions()
        {
            return _client.GetSubscriptions();
        }

        public ApiSubscription UpdateSubscription(ApiSubscription sub)
        {
            return _client.UpdateSubscription(sub);
        }
    }
}