namespace IISDataServiceCLient
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Rebtel : DbContext
    {
        public Rebtel()
            : base("name=Rebtel")
        {
        }

        public virtual DbSet<Subscriptions> Subscriptions { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscriptions>()
                .HasKey(e => e.Id)
                .HasMany(e => e.Users)
                .WithMany(e => e.Subscriptions)
                .Map(m => m.ToTable("user_subscription"));
        }
    }
}
