using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DataService.Interfaces;
using Common;
using NLog;
using System.Data.Entity.Validation;

namespace DataService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SubscriptionService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SubscriptionService.svc or SubscriptionService.svc.cs at the Solution Explorer and start debugging.
    public class SubscriptionService : ISubscriptionService
    {
        private Logger log = LogManager.GetCurrentClassLogger();
        //Valde att köra på 2 GetSubscription istället för en Guid? subId. Det är för att det var lättare ur ett förvaltningsperspektiv. Blev kluddig kod pga cast och iterators.
        public ApiSubscription GetSubscription(Guid subscriptionId)
        {
            log.Debug("GetSubscription(subscriptionId={id}))", subscriptionId);
            using (rebtelEntities container = new rebtelEntities())
            {
                var entitySub = container.Subscriptions.FirstOrDefault(s => s.Id == subscriptionId);
                return entitySub.ToApiSubscription();
            }
        }
        public IEnumerable<ApiSubscription> GetSubscriptions()
        {
            log.Debug("GetSubscriptions()");
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
        public void DeleteSubscription(Guid subscriptionId)
        {
            log.Debug("DeleteSubscription(subscriptionId={id} )", subscriptionId);
            using (rebtelEntities container = new rebtelEntities())
            {
                try
                {
                    var sub = container.Subscriptions.Find(subscriptionId);
                    container.Subscriptions.Remove(sub);
                    container.SaveChanges();
                }
                catch (Exception ex)
                {
                    log.Error("DeleteSubscription(subscriptionId={id}) kastade: {@ex}", subscriptionId,ex);
                }//if its there its already deleted 
            }
        }

        public ApiSubscription UpdateSubscription(ApiSubscription subValues)
        {
            log.Debug("UpdateSubscription(ApiSubscription={@val})", subValues);
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
                sub.Name = string.IsNullOrWhiteSpace(subValues.Name) ? sub.Name : subValues.Name;
                sub.Price = subValues.Price > 0 ? subValues.Price : sub.Price;
                sub.PriceIncVatAmount = sub.Price * 1.25m;
                sub.UrlFriendly = string.IsNullOrWhiteSpace(subValues.Name) ? sub.UrlFriendly : Utilities.ToUrlFriendlyIndentifier(subValues.Name);
                container.SaveChanges();
                return sub.ToApiSubscription();
            }
        }

        public ApiSubscription CreateSubscription(CreateSubscriptionDTO subValues)
        {
            log.Debug("CreateSubscription(CreateSubscriptionDTO={@subValues} )",subValues);
            Subscription sub = null;
            using (rebtelEntities container = new rebtelEntities())
            {
                try
                {

                    sub = container.Subscriptions.Add(subValues.ToEntitySubscription());
                    
                    container.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        log.Info("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            log.Error("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("CreateSubscription() kastade: {@ex}", ex.GetType());
                    log.Error("Message: {@ex}", ex.Message);
                    while (ex.InnerException != null)
                    {
                        log.Error("InnerExceptionMessage: {@ex}", ex.InnerException.Message);
                        ex = ex.InnerException;
                    }
                    
                }
                
            }
            return sub.ToApiSubscription();
        }
    }
}
