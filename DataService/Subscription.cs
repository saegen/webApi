//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Subscription
    {
        public Subscription()
        {
            this.Users = new HashSet<User>();
            this.Id = Guid.NewGuid();
        }

        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal PriceIncVatAmount { get; set; }
        public int CallMinutes { get; set; }
        public string UrlFriendly { get; set; }
    
        public virtual ICollection<User> Users { get; set; }
    }
}
