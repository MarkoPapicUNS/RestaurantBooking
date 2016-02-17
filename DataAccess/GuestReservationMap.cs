using Guest.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class GuestReservationMap : EntityTypeConfiguration<GuestReservation>
    {
        public GuestReservationMap()
        {
            HasKey(gr => new { gr.RestaurantId, gr.GuestUsername, gr.TableNumber, gr.Time });
            HasRequired(gr => gr.Guest).WithMany(r => r.Reservations).HasForeignKey(t => t.GuestUsername);
        }
    }
}
