namespace Common
{
    using System;
    using System.ComponentModel.DataAnnotations;
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

        //[DataMember(IsRequired = true)] denna plockades inte upp i SoapUI men wsdl:en har tagit bort minOcurrs
        [DataMember]
        public System.Guid Id { get; set; }
        [DataMember]
        [Required]
        public string Name { get; set; }
        [DataMember]
        [Required]
        public decimal Price { get; set; }
        [DataMember]
        public decimal PriceIncVatAmount { get; set; }
        [DataMember]
        public int CallMinutes { get; set; }
        [DataMember]
        public string UrlFriendly { get; set; }        
    }
}
