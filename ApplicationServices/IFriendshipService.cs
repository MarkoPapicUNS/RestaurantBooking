using ApplicationServices.Dtos;
using Guest.Domain;

namespace ApplicationServices
{
    public interface IFriendshipService
    {
        ActionResultDto SendFriendRequest(string senderUsername, string receiverUsername);
        FriendshipDto GetFriendship(string requesterUsername, string responderUsername);
    }
}