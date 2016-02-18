using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guest.Domain;

namespace DataAccess
{
	class ReservationInvitationMap : EntityTypeConfiguration<ReservationInvitation>
	{
		public ReservationInvitationMap()
		{
			HasKey(ri => new {ri.GuestReservationId, ri.InvitedGuestUsername});
			HasRequired(r => r.GuestReservation).WithMany(r => r.Invitations).HasForeignKey(t => t.GuestReservationId);
			HasRequired(g => g.InvitedGuest).WithMany(g => g.ReservationInvitations).HasForeignKey(t => t.InvitedGuestUsername);
		}
	}
}
