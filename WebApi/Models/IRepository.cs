using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.SubscriptionServiceReference;
using WebApi.UserServiceReference;

namespace WebApi.Models
{
    public interface IRepository
    {
        ApiSubscription AddUserSubscription(int userId, ApiSubscription subscription);
        int AddUser(ApiUser user);
        ApiUser GetUser(int userId);
        IEnumerable<ApiUser> GetUsers();
        void DeleteUser(int userId);
        ApiUser UpdateUser(ApiUser user);
        IEnumerable<ApiSubscription> GetUserSubscriptions(int userId);
        IEnumerable<ApiSubscription> GetSubscriptions();
        void DeleteSubscription(Guid subscriptionId);
        ApiSubscription UpdateSubscription(ApiSubscription sub);
    }
}
