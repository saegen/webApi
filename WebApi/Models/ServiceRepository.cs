using WebApi.Models;
namespace WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    //using WebApi.UserServiceReference;
    using WebApi.SubscriptionServiceReference;
    using WebApi.UserServiceReference;

    //todo: gör generisk eller?
    public class ServiceRepository : ISubscriptionRepo, IUserRepo, IDisposable
    {
        private SubscriptionServiceClient _subClient;
        private UserServiceClient _userClient;

        public ServiceRepository(SubscriptionServiceClient subClient, UserServiceClient userClient)
        {
            if (subClient == null)
            {
                throw new ArgumentNullException("SubscriptionServiceClient");
            }
            if (userClient == null)
            {
                throw new ArgumentNullException("UserServiceClient");
            }
            _userClient = userClient;
        }

        public SubscriptionServiceReference.ApiSubscription AddUserSubscription(int userId, SubscriptionServiceReference.ApiSubscription subscription)
        {
            return _subClient.AddUserSubscription(userId, subscription);
        }

        public int AddUser(ApiUser user)
        {
            return _userClient.AddUser(user);
        }

        public ApiUser GetUser(int userId)
        {
            return _client.GetUser(userId);
        }

        public IEnumerable<ApiUser> GetUsers()
        {
            return _client.GetUsers();
        }

        public void DeleteUser(int userId)
        {
            _client.DeleteUser(userId);
        }

        public ApiUser UpdateUser(ApiUser user)
        {
            return _client.UpdateUser(user);
        }

        public IEnumerable<ApiSubscription> GetUserSubscriptions(int userId)
        {
            return _client.GetUserSubscriptions(userId);
        }


        public IEnumerable<ApiSubscription> GetSubscriptions()
        {
            return _client.GetSubscriptions();
        }

        public void DeleteSubscription(Guid subscriptionId)
        {
            _client.DeleteSubscription(subscriptionId);
        }

        public ApiSubscription UpdateSubscription(ApiSubscription sub)
        {
            return _client.UpdateSubscription(sub);
        }



        public void Dispose()
        {
            if (_subClient != null) { _subClient.Close; }
        }

        public SubscriptionServiceReference.ApiSubscription AddUserSubscription(int userId, SubscriptionServiceReference.ApiSubscription subscription)
        {
            throw new NotImplementedException();
        }

        IEnumerable<SubscriptionServiceReference.ApiSubscription> ISubscriptionRepo.GetUserSubscriptions(int userId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<SubscriptionServiceReference.ApiSubscription> ISubscriptionRepo.GetSubscriptions()
        {
            throw new NotImplementedException();
        }

        public SubscriptionServiceReference.ApiSubscription UpdateSubscription(SubscriptionServiceReference.ApiSubscription sub)
        {
            throw new NotImplementedException();
        }
    }
}