using Guest.Domain;

namespace Guest.Services
{
    public interface IFriendshipService
    {
        void SendRequest(string senderUsername, string recipientUsername);
        Friendship GetFriendship(string requesterUsername, string responderUsername);
    }
}