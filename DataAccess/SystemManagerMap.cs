using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Domain;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SystemManagerMap : EntityTypeConfiguration<SystemManager>
    {
        public SystemManagerMap()
        {
            HasKey(sm => sm.Username);
        }
    }
}
