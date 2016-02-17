using Restaurant.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class MealMap : EntityTypeConfiguration<Meal>
    {
        public MealMap()
        {
            HasKey(m => new { m.RestaurantId, m.Name });
            HasRequired(r => r.Restaurant).WithMany(m => m.Menu).HasForeignKey(t => t.RestaurantId);
        }
    }
}
