using Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class LogMap : EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            HasKey(l => l.Id);
        }
    }
}
