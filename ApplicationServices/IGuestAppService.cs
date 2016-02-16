using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Dtos;

namespace ApplicationServices
{
    public interface IGuestAppService
    {
        IGuestDto GetGuest(string username, string guestUsername);
        IEnumerable<FriendDisplayDto> GetGuests(string username);
//        IEnumerable<FriendDto> GetFriends(string username);
//        IEnumerable<FriendDto> GetFriendRequests(string username);
//        IEnumerable<FriendDto> GetSentFriendRequests(string username);
    }
}
