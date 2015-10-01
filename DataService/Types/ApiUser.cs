namespace DataService.Types
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
        public ApiUser(User entityUser){
            this.Id = entityUser.Id;
            this.FirstName = entityUser.FirstName;
            this.LastName = entityUser.LastName;
            this.Subscriptions = new HashSet<ApiSubscription>();
            foreach (var sub in entityUser.Subscriptions)
            {
                this.Subscriptions.Add(new ApiSubscription(sub));
            }
        }//konverterar till ApiUser med ApiSubs

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
    
        public User ToEntity() 
        {
            var entity = new User() {Id=this.Id, FirstName= this.FirstName,LastName = this.LastName,Email = this.Email };
            foreach (var ApiSub in this.Subscriptions)
            {
                entity.Subscriptions.Add(ApiSub.ToEntity());
            }
            return entity;
        }
        
    }
}