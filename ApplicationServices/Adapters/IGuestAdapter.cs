using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Dtos;

namespace ApplicationServices.Adapters
{
    public interface IGuestAdapter
    {
        FriendDisplayDto AdaptFriendDisplay(Guest.Domain.Guest friend);
        FriendGuestDto AdaptFriendGuest(Guest.Domain.Guest guest, IEnumerable<Guest.Domain.Guest> guestFriends);
        MeDto AdaptMeGuest(Guest.Domain.Guest guest, IEnumerable<Guest.Domain.Guest> guestFriends, IEnumerable<Guest.Domain.Guest> friendRequests, IEnumerable<Guest.Domain.Guest> sentFriendRequests);
        StrangerGuestDto AdaptStrangerGuest(Guest.Domain.Guest guest);
    }
}
