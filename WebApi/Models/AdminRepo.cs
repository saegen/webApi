using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.AdminService;
using Common;
using System.ServiceModel;
using NLog;

namespace WebApi.Models
{
    public class AdminRepo : IAdminRepo
    {
        private Logger log = LogManager.GetCurrentClassLogger();
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
                try
                {
                    _adminClient.Subscribe(userId, subscriptionIds);
                }
                catch (Exception ex)
                {
                    log.Error("AdminRepo: Subscribe: " + userId + ": " + ex.Message);
                    throw;
                }            
        }

        public IEnumerable<ApiUser> GetSubscriptionUsers(Guid subscriptionId)
        {
            ApiUser[] users;
            try
            {
                users = _adminClient.GetSubscriptionUsers(subscriptionId);
            }
            catch (Exception ex) 
            {
                log.Error("AdminRepo: GetSubscriptionUsers: " + subscriptionId + ": " + ex.Message);
                throw;
            }
            return users;
        }

        public IEnumerable<ApiSubscription> GetUserSubscriptions(int userId)
        {
            ApiSubscription[] subs;
            try
            {
                subs = _adminClient.GetUserSubscriptions(userId);
            }
            catch (Exception ex)
            {
                log.Error("AdminRepo: GetUserSubscriptions: " + userId + ": " + ex.Message);
                throw;
            }
            return subs;
        }

        public void Unsubscribe(int userId, Guid subscriptionId)
        {
            try
            {
                _adminClient.Unsubscribe(userId, subscriptionId);
            }
            catch (Exception ex)
            {
                log.Error("AdminRepo: Unsubscribe: " + userId + ", " + subscriptionId + " : " + ex.Message);
                throw;
            }
            
        }

    }
}