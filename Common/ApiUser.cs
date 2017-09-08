using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Web;
    using System.ComponentModel;
    //Konvertera 
    [DataContract]
    public class ApiUser
    {
        public ApiUser()
        {
            Id = 0;
            FirstName = "DummyFirst";
            LastName = "DummyLast";
            Email = "DummyMail";
            Subscriptions = new ApiSubscription[] { new ApiSubscription(), new ApiSubscription() };
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
        public ICollection<ApiSubscription> Subscriptions { get; set; }

        [DataMember]
        public decimal TotalPriceIncVatAmount
        {
            private set { }
            get
            {
                return Subscriptions.Sum(s => s.PriceIncVatAmount);
            }
        }

        [DataMember]
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
