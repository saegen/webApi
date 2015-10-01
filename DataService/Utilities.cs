using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

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
    }
}