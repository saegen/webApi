using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.AdminService;
using Common;

namespace WebApi.Models
{
    public class AdminRepo : IAdminRepo
    {
        private AdminServiceClient _adminClient;

        public AdminRepo(AdminServiceClient adminClient)
        {
            _adminClient = adminClient;
        }
        public void Subscribe(int userId, ApiSubscription[] subscriptions)
        {
            foreach (var sub in subscriptions)
            {
                _adminClient.Subscribe(userId, subscriptions);
            }
        }

        public IEnumerable<ApiUser> GetSubscriptionUsers(Guid subscriptionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApiSubscription> GetUserSubscriptions(int userId)
        {
            return _adminClient.GetUserSubscriptions(userId);
        }

        public void Subscribe(int userId, IEnumerable<ApiSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(int userId, Guid subscriptionId)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserSubscription(int userId, ApiSubscription subscriptionData)
        {
            throw new NotImplementedException();
        }
    }
}