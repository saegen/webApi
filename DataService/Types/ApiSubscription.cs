using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataService.Types
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public partial class ApiSubscription
    {
        public ApiSubscription(Subscription sub)
        {
            this.Id = sub.Id;
            this.Name = sub.Name;
            this.Price = sub.Price;
            this.PriceIncVatAmount = sub.PriceIncVatAmount;
            this.CallMinutes = sub.CallMinutes;
            this.UrlFriendly = Utilities.toUrlFriendlyIndentifier(sub.Name);
        }
        //Test subscription - denna ska inte vara med alls!!! Använd Moq eller nåt
        public ApiSubscription()
        {
            this.Id = Guid.NewGuid();
            this.Name = "dummyName";
            this.Price = 11m;
            this.PriceIncVatAmount = 22.5m;
            this.CallMinutes = 10;
            this.UrlFriendly = "friendly";
        }
        
        [DataMember]
        public System.Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public decimal PriceIncVatAmount { get; set; }
        [DataMember]
        public int CallMinutes { get; set; }
        [DataMember]
        public string UrlFriendly { get; set; }
        
        public Subscription ToEntity()
        {
            return new Subscription() { Id = this.Id, Name = this.Name, Price = this.Price, PriceIncVatAmount = this.PriceIncVatAmount, CallMinutes=this.CallMinutes,UrlFriendly = this.UrlFriendly };
        }
    }
}