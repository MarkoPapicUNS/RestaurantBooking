using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guest.Domain;
using Restaurant.Domain;

namespace DataAccess
{
    public class RestaurantBookingContext : DbContext
    {
        public DbSet<Guest.Domain.Guest> Guests { get; set; }
        public DbSet<Friendship> Frendships { get; set; }
        public DbSet<Guest.Domain.GuestReservation> GuestReservations { get; set; }
        public DbSet<Restaurant.Domain.Restaurant> Restaurants { get; set; }
        public DbSet<Restaurant.Domain.RestaurantReservation> RestarurantReservations { get; set; }
        public DbSet<Table> RestaurantTables { get; set; }
        public DbSet<Meal> RestaurantMeals { get; set; }

        public RestaurantBookingContext()
            : base("RestaurantBookingContext")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.Add(new GuestMap());
            modelBuilder.Configurations.Add(new FriendshipMap());
            modelBuilder.Configurations.Add(new GuestReservationMap());
            modelBuilder.Configurations.Add(new RestaurantMap());
            modelBuilder.Configurations.Add(new RestaurantReservationMap());
            modelBuilder.Configurations.Add(new TableMap());
            modelBuilder.Configurations.Add(new MealMap());            
        }
    }
}
