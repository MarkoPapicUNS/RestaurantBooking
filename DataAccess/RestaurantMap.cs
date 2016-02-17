using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class RestaurantMap : EntityTypeConfiguration<Restaurant.Domain.Restaurant>
    {
        public RestaurantMap()
        {
            HasKey(r => r.RestaurantId);
        }
    }
}
