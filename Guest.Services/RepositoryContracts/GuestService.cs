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
            return _repository.All();
        }

        public Domain.Guest GetGuest(string username)
        {
            return _repository.Find(username);
        }

        /*public IQueryable<Domain.Guest> GetRelatedGuests(string username)
        {
            var guest = _repository.Find(username);
            var requesterUsernames = guest.ReceivedFriendships.Select(rf => rf.RequesterUsername);
            var responderUsernames = guest.RequestedFriendships.Select(rf => rf.ResponderUsername);
            var friendUsernames = requesterUsernames.Union(responderUsernames);
            var friends = _repository.All().Where(f => friendUsernames.Contains(f.Username));
            return friends;
        }*/

        public IQueryable<Domain.Guest> GetFriends(string username)
        {
            var guest = _repository.Find(username);
            var requesterUsernames = guest.ReceivedFriendships.Where(rf => rf.Status == FriendshipStatus.Active).Select(rf => rf.RequesterUsername);
            var responderUsernames = guest.RequestedFriendships.Where(rf => rf.Status == FriendshipStatus.Active).Select(rf => rf.ResponderUsername);
            var friendUsernames = requesterUsernames.Union(responderUsernames);
            var friends = _repository.All().Where(f => friendUsernames.Contains(f.Username));
            return friends;
        }

        public IQueryable<Domain.Guest> GetFriendRequests(string username)
        {
            if (username == null)
                throw new ArgumentNullException("username");

            var guest = _repository.Find(username);
            var usernames = guest.RequestedFriendships.Where(rf => rf.Status == FriendshipStatus.RequestPending).Select(rf => rf.ResponderUsername);
            return _repository.All().Where(g => usernames.Contains(g.Username));

        }

        public IQueryable<Domain.Guest> GetSentFriendRequests(string username)
        {
            if (username == null)
                throw new ArgumentNullException("username");

            var guest = _repository.Find(username);
            var usernames = guest.ReceivedFriendships.Where(rf => rf.Status == FriendshipStatus.RequestPending).Select(rf => rf.RequesterUsername);
            return _repository.All().Where(g => usernames.Contains(g.Username));
        }
    }
}
