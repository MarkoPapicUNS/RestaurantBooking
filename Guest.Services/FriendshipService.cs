using System;
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
                var guest = _repository.Get(recipientUsername);
                guest.ReceivedFriendships.Add(friendRequest);
                _repository.Commit();
            }
            catch (Exception e)
            {
                throw new FriendshipException("Not possible to process this request.");
            }
        }

        public Friendship GetFriendship(string requesterUsername, string responderUsername)
        {
            if (requesterUsername == null)
                throw new ArgumentNullException("requesterUsername");
            if (responderUsername == null)
                throw new ArgumentNullException("responderUsername");


            var friendship = _repository.GetFriendship(requesterUsername, responderUsername);
            if(friendship == null)
                throw new FriendshipException("Friendship doesn't exist");
            return friendship;
        }
    }
}
