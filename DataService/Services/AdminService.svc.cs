using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.Entity;
namespace DataService
{
    using DataService.Interfaces;
    using Common;
    using NLog;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AdminService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AdminService.svc or AdminService.svc.cs at the Solution Explorer and start debugging.
    public class AdminService : IAdminService
    {
        private Logger log = LogManager.GetCurrentClassLogger();
        public void Subscribe(int userId, IEnumerable<Guid> subscriptionIds)
        {
            if (userId < 1)
            {
                throw new ArgumentException("userId: " + userId + ". Is invalid");
            }
            using (rebtelEntities container = new rebtelEntities())
            {
                var user = container.Users.FirstOrDefault(x => x.Id == userId);
                if (user == null)
                {
                    throw new KeyNotFoundException("No such user");
                }
                foreach (var id in subscriptionIds)
                {
                    try
                    {
                        var sub = container.Subscriptions.FirstOrDefault(x => x.Id == id);
                        user.Subscriptions.Add(sub);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                container.SaveChanges();
            }
        }

        public void Unsubscribe(int userId, Guid subscriptionId)
        {
            if (subscriptionId == null || subscriptionId == Guid.Empty)
            {
                throw new ArgumentNullException("Missing subscriptionId=" + subscriptionId);
            }
            using (rebtelEntities container = new rebtelEntities())
            {
                var user = container.Users.Find(userId);
                if (user == null)
                {
                    throw new ArgumentNullException("No such user");
                }
                var sub = user.Subscriptions.Single(x => x.Id == subscriptionId);
                if (sub == null)
                {
                    throw new FaultException("No such subscription");
                }
                user.Subscriptions.Remove(sub);
                container.SaveChanges();
            }
        }

        public void UnsubscribeAll(int userId)
        {
            using (rebtelEntities container = new rebtelEntities())
            {
                var user = container.Users.Find(userId);
                user.Subscriptions.Clear();
                container.SaveChanges();
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
                    yield return sub.ToApiSubscription(); 
                }
            }
        }

        public IEnumerable<ApiUser> GetSubscriptionUsers(Guid subscriptionId)
        {
            List<ApiUser> users = new List<ApiUser>();
            using (rebtelEntities container = new rebtelEntities())
            {
                //Utilities.ToUrlFriendlyIndentifier(subscription.Name);
                var sub = container.Subscriptions.Find(subscriptionId);
                if (sub == null)
                {
                    log.Info("Kunde inte hitta prenumeration med id={subscriptionId}", subscriptionId);
                    yield return null;
                    
                }
                foreach (var user in sub.Users)
                {
                    yield return user.ToApiUser();
                    
                }
            }
        }



        /*
        public ApiSubscription AddSubscriptions(int userId, IEnumerable<ApiSubscription> subs)
        {
            if (subs == null || !subs.Any())
            {
                throw new ArgumentNullException("subs", "No subscriptions to add");
            }
            using (rebtelEntities container = new rebtelEntities())
            {
                //Utilities.ToUrlFriendlyIndentifier(subscription.Name);
                var user = container.Users.Find(userId);

                if (user == null)
                {
                    throw new ArgumentNullException("userId", "No such user");
                }
                foreach (var sub in subs)
                {
                    sub.UrlFriendly = Utilities.ToUrlFriendlyIndentifier(sub.Name);
                    Utilities.ToEntitySubscription(sub);
                    user.Subscriptions.Add(Utilities.ToEntitySubscription(sub));
                }
                container.SaveChanges();
                return subs.First();
            }
        }

        */

    }

}
