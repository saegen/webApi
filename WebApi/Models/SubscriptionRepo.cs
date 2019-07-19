using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using NLog;
using WebApi.SubscriptionService;

namespace WebApi.Models
{
    public class SubscriptionRepo : ISubscriptionRepo
    {
        private SubscriptionServiceClient _client;
        private Logger log = LogManager.GetCurrentClassLogger();

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


        public ApiSubscription CreateSubscription(CreateSubscriptionDTO subscription)
        {

            return _client.CreateSubscription(subscription);
        }

        public void DeleteSubscription(Guid subscriptionId)
        {
            _client.DeleteSubscription(subscriptionId);
        }

        public ApiSubscription GetSubscription(Guid subscriptionId)
        {
            ApiSubscription sub;
            try
            {
                sub = _client.GetSubscription(subscriptionId);
            }
            catch (Exception ex)
            {
                log.Error("SubscriptionRepo: GetSubscription: " + subscriptionId + ": " + ex.Message);
                throw;
            }
            return sub;
        }

        public IEnumerable<ApiSubscription> GetSubscriptions()
        {
            ApiSubscription[] subs;
            try
            {
                subs = _client.GetSubscriptions();
            }
            catch (Exception ex)
            {
                log.Error("SubscriptionRepo: GetSubscriptions: " + ex.Message);
                throw;
            }
            return subs;
        }

        public ApiSubscription UpdateSubscription(UpdateSubscriptionDTO sub)
        {
            return _client.UpdateSubscription(sub);
        }
    }
}