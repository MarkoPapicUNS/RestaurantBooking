using System.Collections.Generic;
using ApplicationServices.Dtos;

namespace ApplicationServices
{
    public interface IFriendshipAppService
    {
        ActionResultDto SendFriendRequest(string senderUsername, string receiverUsername);
        string GetFriendRequest(string senderUsername, string recipientUsername);
//        IEnumerable<FriendshipDto> GetFriendRequests(string recipientUsername);
//        IEnumerable<FriendshipDto> GetSentFriendRequests(string senderUsername);
        ActionResultDto RemoveFriendship(string username, string friendUsername);
    }
}