using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class GuestMap : EntityTypeConfiguration<Guest.Domain.Guest>
    {
        public GuestMap()
        {
            HasKey(g => g.Username);
        }
    }
}
