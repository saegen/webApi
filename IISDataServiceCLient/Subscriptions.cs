namespace IISDataServiceCLient
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Subscriptions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Subscriptions()
        {
            Users = new HashSet<Users>();
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal PriceIncVatAmount { get; set; }

        public int CallMinutes { get; set; }

        public string UrlFriendly { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users { get; set; }
    }
}
