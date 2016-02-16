using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Dtos;

namespace ApplicationServices.Adapters
{
    public class GuestAdapter : IGuestAdapter
    {
        public FriendDisplayDto AdaptFriendDisplay(Guest.Domain.Guest friend)
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
                Friends = guestFriends.Select(gf => AdaptFriendDisplay(gf)),
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
                Friends = guestFriends.Select(gf => AdaptFriendDisplay(gf)),
                FriendRequests = friendRequests.Select(fr => AdaptFriendDisplay(fr)),
                SentFriendRequests = sentFriendRequests.Select(sfr => AdaptFriendDisplay(sfr))
            };
        }

        public StrangerGuestDto AdaptStrangerGuest(Guest.Domain.Guest guest)
        {
            return new StrangerGuestDto
            {
                Relation = GuestRelation.Stranger,
                Username = guest.Username,
                DisplayName =
                    guest.DisplayFullName ? string.Format("{0} {1}", guest.FirstName, guest.LastName) : guest.Username,
                Picture = guest.Picture
            };
        }
    }
}
