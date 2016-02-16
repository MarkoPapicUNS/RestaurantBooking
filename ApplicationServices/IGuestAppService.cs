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
        IEnumerable<FriendDto> GetFriends(string username);
        IEnumerable<FriendRequestDto> GetFriendRequests(string username);
        IEnumerable<FriendRequestDto> GetSentFriendRequests(string username);
    }
}
