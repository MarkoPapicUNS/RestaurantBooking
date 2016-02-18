using System.Collections.Generic;
using Guest.Domain;
using Shared;

namespace ApplicationServices.Dtos
{
    public class FriendGuestDto : IGuestDto
    {
        public GuestRelation Relation { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public Gender Gender { get; set; }
        public string Picture { get; set; }
        public IEnumerable<FriendDisplayDto> Friends { get; set; }
		public IEnumerable<GuestReservation> Reservations { get; set; } 
    }
}