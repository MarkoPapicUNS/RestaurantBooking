using System.Collections.Generic;
using Guest.Domain;

namespace Guest.Services
{
    public interface IFriendshipService
    {
        void SendRequest(string senderUsername, string recipientUsername);
        Friendship GetFriendRequest(string senderUsername, string recipientUsername);
        IEnumerable<Friendship> GetFriendRequests(string recipientUsername);
        IEnumerable<Friendship> GetSentFriendRequests(string senderUsername);
        IEnumerable<Domain.Guest> GetFriends(string username);
        void RemoveFriendship(string username, string friendUsername);
    }
}