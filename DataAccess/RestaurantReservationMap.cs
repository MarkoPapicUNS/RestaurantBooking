using Restaurant.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class RestaurantReservationMap : EntityTypeConfiguration<RestaurantReservation>
    {
        public RestaurantReservationMap()
        {
            HasKey(rr => new { rr.RestaurantId, rr.GuestUsername, rr.TableNumber, rr.Time });
            HasRequired(rr => rr.Restaurant).WithMany(r => r.Reservations).HasForeignKey(t => t.RestaurantId);
        }
    }
}
