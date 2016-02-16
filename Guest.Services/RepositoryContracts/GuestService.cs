using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guest.Domain;

namespace Guest.Services.RepositoryContracts
{
    public class GuestService : IGuestService
    {
        private IGuestRepository _repository;

        public GuestService(IGuestRepository repository)
        {
            _repository = repository;
        }

        public IQueryable<Domain.Guest> GetGuests()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Domain.Guest> GetFriends(string username)
        {
            var guest = _repository.Find(username);
            var requesterUsernames = guest.ReceivedFriendships.Where(rf => rf.Status == FriendshipStatus.Active).Select(rf => rf.RequesterUsername);
            var responderUsernames = guest.RequestedFriendships.Where(rf => rf.Status == FriendshipStatus.Active).Select(rf => rf.ResponderUsername);
            var friendUsernames = requesterUsernames.Union(responderUsernames);
            var friends = _repository.All().Where(f => friendUsernames.Contains(f.Username));
            return friends;
        }

        public IEnumerable<Friendship> GetFriendRequests(string username)
        {
            if (username == null)
                throw new ArgumentNullException("username");

            var guest = _repository.Find(username);
            return guest.ReceivedFriendships.Where(rf => rf.Status == FriendshipStatus.RequestPending);
        }

        public IEnumerable<Friendship> GetSentFriendRequests(string username)
        {
            if (username == null)
                throw new ArgumentNullException("username");

            var guest = _repository.Find(username);
            return guest.RequestedFriendships.Where(rf => rf.Status == FriendshipStatus.RequestPending);
        }
    }
}
