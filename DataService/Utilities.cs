using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Common;

namespace DataService
{
    public static class Utilities
    {
        public static string ToUrlFriendlyIndentifier(string toFriendly)
        {
            toFriendly = toFriendly.Trim();
            //byter till unicode binary så apostrofer mm försvinner.
            toFriendly = toFriendly.Normalize(); 
            // make it all lower case
            toFriendly = toFriendly.ToLower();
            // remove entities
            toFriendly = Regex.Replace(toFriendly, @"&\w+;", "");
            // remove anything that is not letters, numbers, dash, or space
            toFriendly = Regex.Replace(toFriendly, @"[^a-z0-9\-\s]", "");
            // replace spaces
            toFriendly = toFriendly.Replace(' ', '-');
            // collapse dashes
            toFriendly = Regex.Replace(toFriendly, @"-{2,}", "-");
            // trim excessive dashes at the beginning
            toFriendly = toFriendly.TrimStart(new[] { '-' });
            // if it's too long, clip it
            if (toFriendly.Length > 80)
                toFriendly = toFriendly.Substring(0, 79);
            // remove trailing dashes
            toFriendly = toFriendly.TrimEnd(new[] { '-' });
            return toFriendly;
        }

        public static Subscription ToEntitySubscription(ApiSubscription source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source", "ApiSubscription must not be null");
            }
            return new Subscription() {
                Id = source.Id,
                Name = source.Name,
                Price = source.Price,
                PriceIncVatAmount = source.PriceIncVatAmount,
                CallMinutes = source.CallMinutes,
                UrlFriendly = source.UrlFriendly
            };
        }

        public static Subscription ToEntitySubscription(this CreateSubscriptionDTO sub)
        {
            if (sub == null)
            {
                throw new ArgumentNullException("this", "CreateSubscriptionDTO must not be null");
            }
            Subscription suben = new Subscription()
            {
                Id = Guid.NewGuid(),
                Name = sub.Name,
                Price = sub.Price,
                PriceIncVatAmount = sub.Price * 1.25m,
                CallMinutes = sub.CallMinutes,
                UrlFriendly = ToUrlFriendlyIndentifier(sub.Name)
            };
            return suben;
        }

        public static Subscription ExToEntitySubscription(this ApiSubscription source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("this", "ApiSubscription must not be null");
            }
            return new Subscription()
            {
                Id = source.Id,
                Name = source.Name,
                Price = source.Price,
                PriceIncVatAmount = source.PriceIncVatAmount,
                CallMinutes = source.CallMinutes,
                UrlFriendly = source.UrlFriendly
            };
        }
        public static ApiSubscription ToApiSubscription(Subscription source)
        {
            if (source == null)
            {
                return null;
            }
            return new ApiSubscription() {
                Id = source.Id,
                Name = source.Name,
                Price = source.Price,
                PriceIncVatAmount = source.PriceIncVatAmount,
                CallMinutes = source.CallMinutes,
                UrlFriendly = Utilities.ToUrlFriendlyIndentifier(source.Name)
            };
        }
        /// <summary>
        /// Todo
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ApiSubscription ExToApiSubscription(this Subscription source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("this", "Subscription must not be null");
            }
            return new ApiSubscription()
            {
                Id = source.Id,
                Name = source.Name,
                Price = source.Price,
                PriceIncVatAmount = source.PriceIncVatAmount,
                CallMinutes = source.CallMinutes,
                UrlFriendly = Utilities.ToUrlFriendlyIndentifier(source.Name)
            };
        }

        public static User ToEntityUser(this CreateUserDTO user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("this", "User must not be null");
            }
            return new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }
        public static User ToEntityUser(ApiUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user", "ApiUser must not be null");
            }
            var entityUser = new User() { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email };
            foreach (var ApiSub in user.Subscriptions)
            {
                var sub = Utilities.ToEntitySubscription(ApiSub);
                entityUser.Subscriptions.Add(sub);
            }
            return entityUser;
        }

        public static ApiUser ToApiUser(User entityUser)
        {
            if (entityUser == null)
            {
                return null;
            }
            var apiUser = new ApiUser()
            {
                Id = entityUser.Id,
                FirstName = entityUser.FirstName,
                LastName = entityUser.LastName,
                Email = entityUser.Email,
                Subscriptions = new HashSet<ApiSubscription>()

            };
            foreach (var sub in entityUser.Subscriptions)
            {
                apiUser.Subscriptions.Add(new ApiSubscription()
                {
                    Id = sub.Id,
                    Name = sub.Name,
                    Price = sub.Price,
                    PriceIncVatAmount = sub.PriceIncVatAmount,
                    CallMinutes = sub.CallMinutes,
                    UrlFriendly = Utilities.ToUrlFriendlyIndentifier(sub.Name)
                });
            }
            return apiUser;
        }
    }
}