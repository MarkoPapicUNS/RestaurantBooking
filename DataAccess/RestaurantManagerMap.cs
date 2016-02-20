using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain;

namespace DataAccess
{
    public class RestaurantManagerMap : EntityTypeConfiguration<RestaurantManager>
    {
        public RestaurantManagerMap()
        {
            HasKey(rm => rm.Username);
            HasRequired(rm => rm.Restaurant).WithMany(r => r.Managers).HasForeignKey(rm => rm.RestaurantId);
        }
    }
}
