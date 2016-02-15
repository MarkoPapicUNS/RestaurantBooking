using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guest.Domain;

namespace DataAccess
{
    class FriendshipMap : EntityTypeConfiguration<Friendship>
    {
        public FriendshipMap()
        {
            HasKey(fr => new { fr.RequesterUsername, fr.ResponderUsername });
            HasRequired(s => s.Requester).WithMany(r => r.RequestedFriendships).HasForeignKey(t => t.RequesterUsername);
            HasRequired(r => r.Responder).WithMany(fr => fr.ReceivedFriendships).HasForeignKey(t => t.ResponderUsername);
        }
    }
}
