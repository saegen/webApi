namespace Common
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public partial class ApiSubscription
    {
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
    }
}
