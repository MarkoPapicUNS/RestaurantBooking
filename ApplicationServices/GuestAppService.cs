using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Adapters;
using ApplicationServices.Dtos;
using Guest.Services;

namespace ApplicationServices
{
    public class GuestAppService : IGuestAppService
    {
        private IGuestService _guestService;
        private IGuestAdapter _adapter;


        public GuestAppService(IGuestService guestService, IGuestAdapter adapter)
        {
            _guestService = guestService;
            _adapter = adapter;
        }

        public IGuestDto GetGuest(string username, string guestUsername)
        {
            //var me = _guestService.GetGuest(username); I decided to trust controller
            var guest = _guestService.GetGuest(guestUsername);
            if (guest == null)
                return null;
            var guestFriends = _guestService.GetFriends(guestUsername);
            if (username == guest.Username)
            {
                var friendRequests = _guestService.GetFriendRequests(guestUsername);
                var sentFriendRequests = _guestService.GetSentFriendRequests(guestUsername);
                return _adapter.AdaptMeGuest(guest, guestFriends, friendRequests, sentFriendRequests);
            }
            else if (guestFriends.Any(gf => gf.Username == username))
                return _adapter.AdaptFriendGuest(guest, guestFriends);
            else
                return _adapter.AdaptStrangerGuest(guest);
        }

        public IEnumerable<FriendDisplayDto> GetGuests(string username)
        {
            var guests = _guestService.GetGuests().ToArray();
            return guests.Select(g => _adapter.AdaptFriendDisplay(g));
        }

        /*public IEnumerable<FriendDto> GetFriends(string username)
        {
            var friends = _guestService.GetFriends(username).ToArray();
            return friends.Select(f => _adapter.AdaptFriend(f));
        }

        public IEnumerable<FriendDto> GetFriendRequests(string username)
        {
            var friendShips = _guestService.GetFriendRequests(username);
            return friendShips.Select(f => _adapter.AdaptFriend(f));
        }

        public IEnumerable<FriendDto> GetSentFriendRequests(string username)
        {
            var friendShips = _guestService.GetFriendRequests(username);
            return friendShips.Select(f => _adapter.AdaptFriend(f));
        }*/
    }
}
