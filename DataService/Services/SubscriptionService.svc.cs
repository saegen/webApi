﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DataService.Interfaces;
using Common;

namespace DataService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SubscriptionService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SubscriptionService.svc or SubscriptionService.svc.cs at the Solution Explorer and start debugging.
    public class SubscriptionService : ISubscriptionService
    {
        

        public IEnumerable<ApiSubscription> GetSubscriptions()
        {
            using (rebtelEntities container = new rebtelEntities())
            {
                foreach (var sub in container.Subscriptions)
                {
                    yield return new ApiSubscription()
                    {
                        Id = sub.Id,
                        Name = sub.Name,
                        Price = sub.Price,
                        PriceIncVatAmount = sub.PriceIncVatAmount,
                        CallMinutes = sub.CallMinutes,
                        UrlFriendly = Utilities.ToUrlFriendlyIndentifier(sub.Name)
                    };
                }
            }
        }
    
        //Ska denna vara på User eller båda eller inte alls? Den får vara kvar
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
                    yield return sub.ExToApiSubscription(); //Utilities.ToApiSubscription(sub);
                }
            }
        }

        public void DeleteSubscription(Guid subscriptionId)
        {
            using (rebtelEntities container = new rebtelEntities())
            {
                try
                {
                    var sub = container.Subscriptions.Find(subscriptionId);
                    container.Subscriptions.Remove(sub);
                    container.SaveChanges();
                }
                catch (Exception)
                {
                    //todo add logging  
                }//if its there its already deleted 
            }
        }

        public ApiSubscription UpdateSubscription(ApiSubscription subValues)
        {
            if (subValues == null)
            {
                throw new ArgumentNullException("subValues", "Subscription missing");
            }
            using (rebtelEntities container = new rebtelEntities())
            {
                var sub = container.Subscriptions.Find(subValues.Id);
                if (sub == null)
                {
                    throw new FaultException("No such subscription");
                }
                sub.Name = subValues.Name;
                sub.Price = subValues.Price;
                sub.PriceIncVatAmount = subValues.PriceIncVatAmount;
                sub.UrlFriendly = Utilities.ToUrlFriendlyIndentifier(subValues.Name);
                container.SaveChanges();
                return sub.ExToApiSubscription(); //Utilities.ToApiSubscription(sub);                    
            }
        }

        public int CreateSubscription(ApiSubscription subValues)
        {
            using (rebtelEntities container = new rebtelEntities())
            {
                container.Subscriptions.Add(subValues.ExToEntitySubscription());
                container.SaveChanges();
            }
            return 1;
        }
    }
}