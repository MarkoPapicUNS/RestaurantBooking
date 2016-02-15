using System;
using System.Collections.Generic;
using System.Linq;
using Guest.Domain;
using Guest.Services.Exceptions;
using Guest.Services.RepositoryContracts;

namespace Guest.Services
{
    public class FriendshipService : IFriendshipService
    {
        private IGuestRepository _repository;

        public FriendshipService(IGuestRepository repository)
        {
            _repository = repository;
        }

        public void SendRequest(string senderUsername, string recipientUsername)
        {
            if (senderUsername == null)
                throw new ArgumentNullException("senderUsername");
            if (recipientUsername == null)
                throw new ArgumentNullException("recipientUsername");

            var friendRequest = new Friendship
            {
                RequesterUsername = senderUsername,
                ResponderUsername = recipientUsername,
                Status = FriendshipStatus.RequestPending
            };
            
            try
            {
                var guest = _repository.Find(recipientUsername);
                guest.ReceivedFriendships.Add(friendRequest);
                _repository.Commit();
            }
            catch (Exception e)
            {
                throw new FriendshipException("Not possible to process this request.");
            }
        }

        public Friendship GetFriendRequest(string senderUsername, string recipientUsername)
        {
            if (senderUsername == null)
                throw new ArgumentNullException("senderUsername");
            if (recipientUsername == null)
                throw new ArgumentNullException("recipientUsername");

            var friendRequest = _repository.GetFriendship(senderUsername, recipientUsername);
            if (friendRequest == null || friendRequest.Status != FriendshipStatus.RequestPending)
                friendRequest = null;
            return friendRequest;
        }

        public IEnumerable<Friendship> GetFriendRequests(string recipientUsername)
        {
            if (recipientUsername == null)
                throw new ArgumentNullException("recipientUsername");

            var guest = _repository.Find(recipientUsername);
            return guest == null ? null : guest.ReceivedFriendships.Where(r => r.Status == FriendshipStatus.RequestPending);
        }

        public IEnumerable<Friendship> GetSentFriendRequests(string senderUsername)
        {
            if (senderUsername == null)
                throw new ArgumentNullException("senderUsername");

            var guest = _repository.Find(senderUsername);
            return guest == null ? null : guest.RequestedFriendships.Where(r => r.Status == FriendshipStatus.RequestPending);
        }

        public IEnumerable<Domain.Guest> GetFriends(string username)
        {
            if (username == null)
                throw new ArgumentNullException("username");

            var friendships = _repository.GetFriendships();
            var respondersUsernames =
                friendships.Where(f => f.RequesterUsername == username && f.Status == FriendshipStatus.Active).Select(f2 => f2.ResponderUsername);
            var requesterUsernames = friendships.Where(f => f.ResponderUsername == username && f.Status == FriendshipStatus.Active).Select(f2 => f2.RequesterUsername);
            var friendsUsernames = respondersUsernames.Union(requesterUsernames);
            return _repository.All().Where(g => friendsUsernames.Contains(g.Username));
        }

        public void RemoveFriendship(string username, string friendUsername)
        {
            if (username == null)
                throw new ArgumentNullException("username");

            var friendship =
                _repository.GetFriendships()
                    .FirstOrDefault(
                        f =>
                            f.RequesterUsername == username && f.ResponderUsername == friendUsername ||
                            f.RequesterUsername == friendUsername && f.ResponderUsername == username);
            _repository.DeleteFriendship(friendship);
            _repository.Commit();
        }
    }
}
