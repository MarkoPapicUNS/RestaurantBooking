using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain;

namespace DataAccess
{
    public class RestaurantRatingMap : EntityTypeConfiguration<RestaurantRating>
    {
        public RestaurantRatingMap()
        {
            HasKey(r => new {r.RestaurantId, r.GuestUsername});
            HasRequired(r => r.Restaurant).WithMany(r => r.Ratings).HasForeignKey(r => r.RestaurantId);
        }
    }
}
