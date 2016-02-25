using System;
using Newtonsoft.Json;

namespace Guest.Domain
{
	public class ReservationInvitation
	{
		/*public string SenderUsername { get; set; }
		public string RestaurantId { get; set; }
		public string RestaurantName { get; set; }
		public string GuestDisplayName { get; set; }
		public string InvitedGuestDisplayName { get; set; }
		public int TableNumber { get; set; }
		public DateTime Time { get; set; }*/
		public int GuestReservationId { get; set; }
		public string InvitedGuestUsername { get; set; }
		public string InvitorUsername { get; set; }
		public string InvitorDisplayName { get; set; }
		public string RestaurantName { get; set; }
		public string RestaurantId { get; set; }
		public ReservationInvitationStatus Status { get; set; }

		//for EntityFramework
		[JsonIgnore]
		public virtual GuestReservation GuestReservation { get; set; }
		[JsonIgnore]
		public virtual Guest InvitedGuest { get; set; }
	}
}
