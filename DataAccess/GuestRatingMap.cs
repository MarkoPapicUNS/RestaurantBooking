using Guest.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class GuestRatingMap : EntityTypeConfiguration<GuestRating>
    {
        public GuestRatingMap()
        {
            HasKey(m => new { m.RestaurantId, m.GuestUsername });
            HasRequired(r => r.Guest).WithMany(m => m.Ratings).HasForeignKey(t => t.GuestUsername);
        }
    }
}
