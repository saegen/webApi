namespace rebtelService1
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Linq;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel;

    [DataContract]
    public partial class PartialUser
    {
        public PartialUser()
        {
            this.Subscriptions = new HashSet<Subscription>();
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public virtual ICollection<Subscription> Subscriptions { get; set; }

        [DataMember]
        [NotMapped]
        [DefaultValue(0)]
        public decimal TotalPriceIncVatAmount
        {
            private set { }
            get
            {
                return Subscriptions.Sum(s => s.PriceIncVatAmount);
            }
        }

        [DataMember]
        [NotMapped]
        [DefaultValue(0)]
        public int TotalCallMinutes
        {
            private set { }
            get
            {
                return Subscriptions.Sum(s => s.CallMinutes);
            }
        }
    }
}
