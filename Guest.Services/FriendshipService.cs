using System;
using System.Collections.Generic;
using System.Linq;
using Guest.Domain;
using Guest.Services.Exceptions;
using Guest.Services.RepositoryContracts;
using Shared;
using System.Threading.Tasks;

namespace Guest.Services
{
    public class FriendshipService : IFriendshipService
    {
        private IFriendshipRepository _repository;
        private ILogger _logger;

        public FriendshipService(IFriendshipRepository repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public void SendRequest(string senderUsername, string recipientUsername)
        {
            if (senderUsername == null)
                throw new ArgumentNullException("senderUsername");
            if (recipientUsername == null)
                throw new ArgumentNullException("recipientUsername");

			//Task.Run( () => _logger.Log(LogMessageType.Notification, string.Format("Started sending request sending friend request from {0} to {1}", senderUsername, recipientUsername)));
	        _logger.Log(LogMessageType.Notification,
		        string.Format("Started sending request sending friend request from {0} to {1}", senderUsername,
			        recipientUsername));

			var friendRequest = new Friendship
            {
                RequesterUsername = senderUsername,
                ResponderUsername = recipientUsername,
                Status = FriendshipStatus.RequestPending
            };
            
            try
            {
                _repository.Insert(friendRequest);
                _repository.Commit();
            }
            catch (Exception e)
            {
                Task.Run(() => _logger.Log(LogMessageType.Error, e.Message));
                throw new FriendshipException("Not possible to process this request.");
            }
        }

        public Friendship GetFriendRequest(string senderUsername, string recipientUsername)
        {
            if (senderUsername == null)
                throw new ArgumentNullException("senderUsername");
            if (recipientUsername == null)
                throw new ArgumentNullException("recipientUsername");

            var friendRequest = _repository.Find(senderUsername, recipientUsername);
            if (friendRequest == null || friendRequest.Status != FriendshipStatus.RequestPending)
                friendRequest = null;
            return friendRequest;
        }

        public void AcceptFriendRequest(string senderUsername, string recipientUsername)
        {
            if (senderUsername == null)
                throw new ArgumentNullException("senderUsername");
            if (recipientUsername == null)
                throw new ArgumentNullException("senderUsername");

            //Task.Run(() => _logger.Log(LogMessageType.Notification, string.Format("Started removing {0} from {1}'s friends", friendUsername, username)));
            _logger.Log(LogMessageType.Notification,
                string.Format("Accepting friend request from {0} to {1}", senderUsername, recipientUsername));
            var friendship =
                _repository.All()
                    .FirstOrDefault(
                        f => f.RequesterUsername == senderUsername && f.ResponderUsername == recipientUsername && f.Status == FriendshipStatus.RequestPending);
            friendship.Status = FriendshipStatus.Active;
            _repository.Commit();
        }

        /*public IEnumerable<Friendship> GetFriendRequests(string recipientUsername)
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

        public IEnumerable<Friendship> GetFriendships(string username)
        {
            if (username == null)
                throw new ArgumentNullException("username");

            var allFriendships = _repository.All();
            var usersFriendships =
                allFriendships.Where(
                    f =>
                        f.Status == FriendshipStatus.Active &&
                        (f.RequesterUsername == username || f.ResponderUsername == username));
            return usersFriendships;
        }*/

        public void RemoveFriendship(string username, string friendUsername)
        {
            if (username == null)
                throw new ArgumentNullException("username");

			//Task.Run(() => _logger.Log(LogMessageType.Notification, string.Format("Started removing {0} from {1}'s friends", friendUsername, username)));
	        _logger.Log(LogMessageType.Notification,
		        string.Format("Started removing {0} from {1}'s friends", friendUsername, username));
			var friendship =
                _repository.All().FirstOrDefault(
                        f =>
                            f.RequesterUsername == username && f.ResponderUsername == friendUsername ||
                            f.RequesterUsername == friendUsername && f.ResponderUsername == username);
            _repository.Delete(friendship);
            _repository.Commit();
        }
    }
}
