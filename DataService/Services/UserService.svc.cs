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
using System.ServiceModel;
using DataService.Interfaces;
using NLog;

namespace DataService
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataService.svc or DataService.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {

        private Logger log = LogManager.GetCurrentClassLogger();
        public ApiUser CreateUser(CreateUserDTO user)
        {
            log.Debug("CreateUser(CreateUserDTO={@userDTO})", user);
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var entityUser = user.ToEntityUser();

            using (rebtelEntities container = new rebtelEntities())
            {

                //If I where to use url-friendly-name I would have done this or maybe have a unique contstraint on the colmn and then add the count in the catch:
                //int count = container.Users.Where(u => u.FirstName == entityUser.FirstName && u.LastName == entityUser.LastName).Count();
                //entityUser.urlFriendly = Utilities.ToUrlFriendlyIndentifier(entityUser.FirstName + "-" + entityUser.LastName);
                //entityUser.urlFriendly += count > 0 ? count.ToString() : "";
                entityUser = container.Users.Add(entityUser);
                container.SaveChanges();
                return entityUser.ToApiUser();
            }
        }

        public IEnumerable<ApiUser> GetUsers()
        {
            log.Debug("GetUsers()");

            using (rebtelEntities container = new rebtelEntities())
            {
                foreach (var user in container.Users.Include("Subscriptions"))
                {
                    ApiUser apiUser = user.ToApiUser();
                    log.Debug("User: {@user}", apiUser);
                    yield return apiUser;
                }
            }
        }

        public ApiUser GetUser(int userId)
        {
            log.Debug("GetUser(userId = {id})", userId);
            using (rebtelEntities container = new rebtelEntities())
            {
                var user = container.Users.Find(userId);
                if (user == null)
                {
                    log.Info("Could not find user with Id: {id}", userId);
                    return null;
                }
                return user.ToApiUser();
            }
        }
        /// <summary>
        /// Medvetet att inte kasta fel. Tar man bort en användare som inte finns är den borta ändå
        /// </summary>
        /// <param name="userId"></param>
        public void DeleteUser(int userId)
        {
            log.Debug("DeleteUser(userId = {id})", userId);
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

        public ApiUser UpdateUser(UpdateUserDTO userValues)
        {
            log.Debug("UpdateUser(UpdateUserDTO={@userDTO})", userValues);
            using (rebtelEntities container = new rebtelEntities())
            {
                var user = container.Users.Find(userValues.Id);
                if (user == null)
                {
                    throw new FaultException("No such user to update");
                }
                user.FirstName = string.IsNullOrWhiteSpace(userValues.FirstName) ? user.FirstName : userValues.FirstName;
                user.LastName = string.IsNullOrWhiteSpace(userValues.LastName) ? user.LastName : userValues.LastName;
                user.Email = string.IsNullOrWhiteSpace(userValues.Email) ? user.Email : userValues.Email;
                container.SaveChanges();
                return user.ToApiUser();
            }
        }
    }
}
