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
        private IFriendshipAdapter _adapter;


        public GuestAppService(IGuestService guestService, IFriendshipAdapter adapter)
        {
            _guestService = guestService;
            _adapter = adapter;
        }

        public IEnumerable<FriendDto> GetFriends(string username)
        {
            var friends = _guestService.GetFriends(username).ToArray();
            return friends.Select(f => _adapter.AdaptFriend(f));
        }

        public IEnumerable<FriendRequestDto> GetFriendRequests(string username)
        {
            var friendShips = _guestService.GetFriendRequests(username);
            return friendShips.Select(f => _adapter.AdaptFriendship(f));
        }

        public IEnumerable<FriendRequestDto> GetSentFriendRequests(string username)
        {
            var friendShips = _guestService.GetFriendRequests(username);
            return friendShips.Select(f => _adapter.AdaptFriendship(f));
        }
    }
}
