using DataService.Interfaces;
using DataService.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace DataService.Services
{
    class UserService : IUser
    {
        public int AddUser(ApiUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var entityUser = user.ToEntity();

            using (rebtelEntities container = new rebtelEntities())
            {
                container.Users.Add(entityUser);
                container.SaveChanges();
                return entityUser.Id;
            }
        }

        public ApiSubscription AddUserSubscription(int userId, ApiSubscription subscription)
        {
            using (rebtelEntities container = new rebtelEntities())
            {
                var user = container.Users.Find(userId);
                if (user == null)
                {
                    throw new ArgumentNullException("No such user");
                }
                subscription.UrlFriendly = Utilities.toUrlFriendlyIndentifier(subscription.Name);
                user.Subscriptions.Add(subscription.ToEntity());
                container.SaveChanges();
                return subscription;
            }
        }

        public void DeleteUser(int userId)
        {
            using (rebtelEntities container = new rebtelEntities())
            {
                User user = container.Users.Find(userId);  //Where(u => u.urlFriendly == urlFriendlyname).FirstOrDefault();
                if (user != null)
                {
                    container.Users.Remove(user);
                    container.SaveChanges();
                }
            }
        }

        public ApiUser GetUser(int userId)
        {
            using (rebtelEntities container = new rebtelEntities())
            {
                var user = container.Users.Find(userId);
                if (user == null)
                {
                    throw new FaultException("No such user");
                }
                return new ApiUser(user);
            }
        }

        public IEnumerable<ApiUser> GetUsers()
        {
            using (rebtelEntities container = new rebtelEntities())
            {
                foreach (var user in container.Users.Include("Subscriptions"))
                {
                    yield return new ApiUser(user);
                }
            }
        }

        public IEnumerable<ApiSubscription> GetUserSubscriptions(int userId)
        {
            using (rebtelEntities container = new rebtelEntities())
            {
                var user = container.Users.Find(userId);
                if (user == null)
                {
                    throw new FaultException("No such user");
                }
                foreach (var sub in user.Subscriptions)
                {
                    yield return new ApiSubscription(sub);
                }
            }
        }

        public ApiUser UpdateUser(ApiUser userValues)
        {
            using (rebtelEntities container = new rebtelEntities())
            {
                var user = container.Users.Find(userValues.Id);
                if (user == null)
                {
                    throw new FaultException("No such user to update");
                }
                user.FirstName = userValues.FirstName;
                user.LastName = userValues.LastName;
                user.Email = userValues.Email;
                container.SaveChanges();
                return new ApiUser(user);
            }
        }
    }
}