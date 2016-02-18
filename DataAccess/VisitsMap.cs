using Guest.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class VisitMap : EntityTypeConfiguration<Visit>
    {
        public VisitMap() {
            HasKey(v => new { v.GuestUsername, v.RestaurantId, v.Time });
            HasRequired(r => r.Guest).WithMany(m => m.Visits).HasForeignKey(t => t.GuestUsername);
        }
    }
}
