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
        #region Extentions
        public static Subscription ToEntitySubscription(this CreateSubscriptionDTO sub)
        {
            if (sub == null)
            {
                throw new ArgumentNullException("this", "CreateSubscriptionDTO must not be null");
            }
            Subscription suben = new Subscription()
            {
                Name = sub.Name,
                Price = sub.Price,
                PriceIncVatAmount = sub.Price * 1.25m,
                CallMinutes = sub.CallMinutes,
                UrlFriendly = ToUrlFriendlyIndentifier(sub.Name)
            };
            return suben;
        }
        public static ApiSubscription ToApiSubscription(this Subscription source)
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
        public static Subscription ToEntitySubscription(this ApiSubscription source)
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
        public static ApiUser ToApiUser(this User source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("this", "User must not be null");
            }
            var user = new ApiUser()
            {
                Id = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Email = source.Email
            };
            foreach (var sub in source.Subscriptions)
            {
                user.Subscriptions.Add(sub.ToApiSubscription());
            }
            return user;
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

        #endregion
             
        
    }
}