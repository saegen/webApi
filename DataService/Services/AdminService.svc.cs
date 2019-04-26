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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AdminService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AdminService.svc or AdminService.svc.cs at the Solution Explorer and start debugging.
    public class AdminService : IAdminService
    {
        public void Subscribe(int userId, IEnumerable<ApiSubscription> subscriptions)
        {
            if (!subscriptions.Any())
            {
                throw new ArgumentNullException("No subscriptions to add");
            }

            using (rebtelEntities container = new rebtelEntities())
            {
                var user = container.Users.Find(userId);
                if (user == null)
                {
                    throw new ArgumentNullException("No such user");
                }
                foreach (var sub in subscriptions)
                {
                    if (container.Subscriptions.Find(sub.Id) != null)
                    {
                        user.Subscriptions.Add(Utilities.ToEntitySubscription(sub));
                    }
                }
                container.SaveChanges();
            }
        }

        public void Unsubscribe(int userId, Guid subscriptionId)
        {
            if (subscriptionId == null || subscriptionId == Guid.Empty)
            {
                throw new ArgumentNullException("No subscriptions");
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
    }

}
