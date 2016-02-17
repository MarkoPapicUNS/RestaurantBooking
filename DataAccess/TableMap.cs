using Restaurant.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class TableMap : EntityTypeConfiguration<Table>
    {
        public TableMap() {
            HasKey(t => new { t.RestaurantId, t.Number });
            HasRequired(t => t.Restaurant).WithMany(m => m.Tables).HasForeignKey(t => t.RestaurantId);
        }
    }
}
