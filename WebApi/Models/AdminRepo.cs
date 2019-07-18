using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.AdminService;
using Common;
using System.ServiceModel;

namespace WebApi.Models
{
    public class AdminRepo : IAdminRepo
    {
        private AdminServiceClient _adminClient;

        public AdminRepo(AdminServiceClient adminClient)
        {
            _adminClient = adminClient;
        }
        ~AdminRepo()
        {
            if (_adminClient != null && _adminClient.State == CommunicationState.Opened)
            {
                _adminClient.Close();
            }
        }

        public void Subscribe(int userId, Guid[] subscriptionIds)
        {
            foreach (var sub in subscriptionIds)
            {
                _adminClient.Subscribe(userId, subscriptionIds);
            }
        }

        public IEnumerable<ApiUser> GetSubscriptionUsers(Guid subscriptionId)
        {
          return  _adminClient.GetSubscriptionUsers(subscriptionId);
        }

        public IEnumerable<ApiSubscription> GetUserSubscriptions(int userId)
        {
            return _adminClient.GetUserSubscriptions(userId);
        }

        public void Unsubscribe(int userId, Guid subscriptionId)
        {
            _adminClient.Unsubscribe( userId,  subscriptionId);
        }

    }
}