using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Dtos;
using ApplicationServices.Models;
using Shared;

namespace ApplicationServices.Adapters
{
    public class GuestAdapter : IGuestAdapter
    {
        public FriendDisplayDto AdaptGuestDisplay(Guest.Domain.Guest friend)
        {
            if (friend == null)
                throw new ArgumentNullException("friend");

            var friendDto = new FriendDisplayDto
            {
                Username = friend.Username,
                DisplayName =
                    friend.DisplayFullName
                        ? string.Format("{0} {1}", friend.FirstName, friend.LastName)
                        : friend.Username,
                Picture = friend.Picture
            };
            return friendDto;
        }

        public FriendGuestDto AdaptFriendGuest(Guest.Domain.Guest guest, IEnumerable<Guest.Domain.Guest> guestFriends)
        {
            return new FriendGuestDto
            {
                Relation = GuestRelation.Friend,
                Username = guest.Username,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                DisplayName =
                    guest.DisplayFullName ? string.Format("{0} {1}", guest.FirstName, guest.LastName) : guest.Username,
                Address = guest.Address,
                Gender = guest.Gender,
                Picture = guest.Picture,
                Friends = guestFriends.Select(gf => AdaptGuestDisplay(gf)),
				Reservations = guest.Reservations.ToArray(),
                Ratings = guest.Ratings.ToArray(),
                Visits = guest.Visits.ToArray()
            };
        }

        public MeDto AdaptMeGuest(Guest.Domain.Guest guest, IEnumerable<Guest.Domain.Guest> guestFriends,
            IEnumerable<Guest.Domain.Guest> friendRequests, IEnumerable<Guest.Domain.Guest> sentFriendRequests)
        {
            return new MeDto
            {
                Relation = GuestRelation.Me,
                Username = guest.Username,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                DisplayName =
                    guest.DisplayFullName ? string.Format("{0} {1}", guest.FirstName, guest.LastName) : guest.Username,
                Address = guest.Address,
                Gender = guest.Gender,
                Picture = guest.Picture,
                Friends = guestFriends.Select(gf => AdaptGuestDisplay(gf)),
                FriendRequests = friendRequests.Select(fr => AdaptGuestDisplay(fr)),
                SentFriendRequests = sentFriendRequests.Select(sfr => AdaptGuestDisplay(sfr)),
				Reservations = guest.Reservations.ToArray(),
				ReservationInvitations = guest.ReservationInvitations,
				SentReservationInvitations = guest.SentReservationInvitations,
                Ratings = guest.Ratings.ToArray(),
                Visits = guest.Visits.ToArray()
            };
        }

        public StrangerGuestDto AdaptStrangerGuest(Guest.Domain.Guest guest, GuestRelation relation)
        {
            return new StrangerGuestDto
            {
                Relation = relation,
                Username = guest.Username,
                DisplayName =
                    guest.DisplayFullName ? string.Format("{0} {1}", guest.FirstName, guest.LastName) : guest.Username,
                Picture = guest.Picture
            };
        }

	    public Guest.Domain.Guest CreateGuestFromProfileModel(ProfileModel data)
	    {
		    return new Guest.Domain.Guest
		    {
			    Username = data.Username,
				FirstName = data.FirstName,
				LastName = data.LastName,
				DisplayFullName = data.DisplayFullName,
				Address = new Address
				{
					City = data.City,
					Number = data.Number,
					Street = data.Street,
					Zip = data.Zip
				},
				Gender = (Gender)data.Gender,
				Picture = data.Picture
		    };
	    }
    }
}
