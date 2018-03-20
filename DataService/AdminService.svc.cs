using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

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
            }
        }

        //public void Unsubscribe(int userId, Guid subscriptionId)
        //{
        //    throw new NotImplementedException();
        //}

        public void Unsubscribe(int userId, IEnumerable<ApiSubscription> subscriptions)
        {
            if (subscriptions.Count() < 1)
            {
                throw new ArgumentNullException("No such user");
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
            }
        }

        public void UnsubscribeAll(int userId)
        {
            throw new NotImplementedException();
        }
    }

}
