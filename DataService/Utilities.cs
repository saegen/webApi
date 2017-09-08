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
        public static string toUrlFriendlyIndentifier(string toFriendly)
        {
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
            return new Subscription() {
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
            return new ApiSubscription() {
                Id = source.Id,
                Name = source.Name,
                Price = source.Price,
                PriceIncVatAmount = source.PriceIncVatAmount,
                CallMinutes = source.CallMinutes,
                UrlFriendly = Utilities.toUrlFriendlyIndentifier(source.Name)
            };
        }

        public static User ToEntityUser(ApiUser user)
        {
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
            var apiUser = new ApiUser()
            {
                Id = entityUser.Id,
                FirstName = entityUser.FirstName,
                LastName = entityUser.LastName,
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
                    UrlFriendly = Utilities.toUrlFriendlyIndentifier(sub.Name)
                });
            }
            return apiUser;
        }
    }
}