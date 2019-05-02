//Since this is my experience with WebApi,REST, MVC and Entity Framework understanding the spec was/is realy the hardest part for me.
//I have multiple questions concerning it:
//Is int an url-friendly-identifier, I thought yes because I have seen on different sites and its also used on several times on
//http://www.asp.net/web-api/overviewhttp://www.asp.net/web-api/overview. I asked a (much) more senior developer than me and he thougt no. Is it?
//I did Utilities class with a method that makes a string url-friendly but I did copy/paste the method body so I'm not taking to much credit for that.
//When I started I choose no authentication, as to make things easy, when I created my Web Api project. (To be honest I didn't even know
//wheter to create it with MVC or an empty Web Api let alone the implications the decision might have. I wasn't quite sure how subscriptions 
//relate to user: ie many-to-many or many-to-one. By looking at the spec I saw (PUT -> /users/subscriptionId) and Create subscription (POST -> /subscriptions)
//which made me think many-to-many [user.Id]-[userId,subId]-[sub.Id] not [user]-[subs:FK userId] as this is. I made that assumption since a subscription gets 
//created separately and I thought such an operation would look like (PUT -> /users/userId/subscriptionId) wich I actually implemented tested using PostMan.
//Now that I have googled and read a lot I'm guessing the spec implies authentication, using a session cookie perhaps. Is this correct?
// Because there is FK userId in table [subs] a subscription cannot exist without a user and I left out the authentication I have altered
//the spec, (a big no no), sorry about that but it's tad bit late for me to change it.
//Add subscription to user (PUT -> /users/subscriptionId) is now Add subscription to user (PUT -> /users/{int:id}/subscriptionId)
//Even tough there are no unit tests I did implement the Repository pattern (of sorts e.g. not using IDependencyResolver) in the Web Api. 
//I am not sure if it's because I lack experience in working with the .edmx in VS but I manually had to change the 
//rebModel.edmx.sql to add the precision on the decimal values and also adding delete on cascade on FK:userId in [subs].
//There is no logging implemented so there are not much try/catch which would normaly be the case.

//All in all I found this task to be very stressful, (especially since Vasco said an experienced developer would do this in 4-7 hours and 
//if took longer than that then something was wrong), but also very fun, interesting and extremly useful in the sense that I got to learn about
//EF, MVC, REST, Web Api and PostMan. Even if you turn me down I would really like to get some constructive feedback and not just a 'no thank you'. 
//Because I'm very eager to learn and I intend to keep on learning more on the subject(s) I would like to take this opportunity to 
//get some real feedback from people who does this every day.

//using DataService.Types;
using Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DataService.Interfaces;
using NLog;

namespace DataService
{
 
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataService.svc or DataService.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService//, ISubscriptionService
    {
        
        private Logger log = LogManager.GetCurrentClassLogger();
        public ApiUser CreateUser(ApiUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var entityUser = Utilities.ToEntityUser(user); 
            
            using (rebtelEntities container = new rebtelEntities())
            {
             
                    //If I where to use url-friendly-name I would have done this or maybe have a unique contstraint on the colmn and then add the count in the catch:
                    //int count = container.Users.Where(u => u.FirstName == entityUser.FirstName && u.LastName == entityUser.LastName).Count();
                    //entityUser.urlFriendly = Utilities.ToUrlFriendlyIndentifier(entityUser.FirstName + "-" + entityUser.LastName);
                    //entityUser.urlFriendly += count > 0 ? count.ToString() : "";
                    container.Users.Add(entityUser);
                    container.SaveChanges();
                    return user;
            }
        }
      
        public IEnumerable<ApiUser> GetUsers()
        {
            log.Debug("GetUsers()");

            using (rebtelEntities container = new rebtelEntities())
            {
                foreach (var user in container.Users.Include("Subscriptions"))
                {
                    var apiUser = Utilities.ToApiUser(user);
                    log.Debug("User: {user}", apiUser);
                    yield return apiUser;
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
                return Utilities.ToApiUser(user); 
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

        public IEnumerable<ApiSubscription> GetUserSubscriptions(int userId)
        {

            //throw new NotImplementedException();
            using (rebtelEntities container = new rebtelEntities())
            {
                var user = container.Users.Find(userId);
                if (user == null)
                {
                    throw new FaultException("No such user");
                }
                foreach (var sub in user.Subscriptions)
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

        //public IEnumerable<ApiSubscription> GetSubscriptions()
        //{
        //    using (rebtelEntities container = new rebtelEntities())
        //    {
        //        foreach (var sub in container.Subscriptions)
        //        {
        //            yield return new ApiSubscription(sub);
        //        }
        //    }
        //}

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
                return Utilities.ToApiUser(user);
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
                catch (Exception) { 
                  //todo add logging  
                }

            }
        }

        public ApiSubscription UpdateSubscription(ApiSubscription subValues)
        {
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
                return new ApiSubscription()
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
}
