using WebApi.Models;
namespace WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using WebApi.UserServiceReference;
    using WebApi.SubscriptionServiceReference;
    //todo: gör generisk eller?
    public class ServiceRepository : IRepository, IDisposable
    {
        private SubscriptionServiceClient _subClient;
        
        public ServiceRepository(SubscriptionServiceClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }
            _subClient = client;
        }

        public ApiSubscription AddUserSubscription(int userId, ApiSubscription subscription)
        {
            return _client.AddUserSubscription(userId, subscription);
        }

        public int AddUser(ApiUser user)
        {
            return _client.AddUser(user);
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
    }
}