using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guest.Domain;

namespace DataAccess
{
    public class RestaurantBookingContext : DbContext
    {
        public DbSet<Guest.Domain.Guest> Guests { get; set; }
        public DbSet<Friendship> Frendships { get; set; }

        public RestaurantBookingContext()
            : base("RestaurantBookingContext")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.Add(new GuestMap());
            modelBuilder.Configurations.Add(new FriendshipMap());
        }
    }
}
