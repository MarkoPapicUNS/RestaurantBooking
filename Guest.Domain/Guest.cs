using System.Collections.Generic;
using Shared;

namespace Guest.Domain
{
    public class Guest : IUser, IAggregateRoot
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool DisplayFullName { get; set; }
        public Address Address { get; set; }
        public Gender Gender { get; set; }
        public string Picture { get; set; }
        public List<Friendship> RequestedFriendships { get; set; }
        public List<Friendship> ReceivedFriendships { get; set; }
        public List<GuestReservation> Reservations { get; set; }
		public List<ReservationInvitation> ReservationInvitations { get; set; }
		public List<ReservationInvitation> SentReservationInvitations { get; set; }
        public List<GuestRating> Ratings { get; set; }
        public List<Visit> Visits { get; set; }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;
            var other = (Guest) obj;
            return Username == other.Username;
        }

        public override int GetHashCode()
        {
            return Username.GetHashCode();
        }
    }
}
