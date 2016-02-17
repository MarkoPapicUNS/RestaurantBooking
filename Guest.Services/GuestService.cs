using System;
using System.Linq;
using Guest.Domain;
using Guest.Services.Exceptions;
using Guest.Services.RepositoryContracts;

namespace Guest.Services
{
    public class GuestService : IGuestService
    {
        private IGuestRepository _repository;

        public GuestService(IGuestRepository repository)
        {
            _repository = repository;
        }

        public IQueryable<Domain.Guest> GetGuests(string username)
        {
            return _repository.All().Where(g => g.Username != username);
        }

        public Domain.Guest GetGuest(string username)
        {
            return _repository.Find(username);
        }

	    public void UpdateProfile(Domain.Guest profileData)
	    {
			if (profileData == null)
				throw new ArgumentNullException("profileData");

		    var guest = _repository.Find(profileData.Username);
			if (guest == null)
				throw new GuestException(string.Format("User {0} doesn't exist.", profileData.Username));
		    guest.FirstName = profileData.FirstName;
		    guest.LastName = profileData.LastName;
		    guest.DisplayFullName = profileData.DisplayFullName;
		    guest.Address = profileData.Address;
		    guest.Gender = profileData.Gender;
		    guest.Picture = profileData.Picture;
			_repository.Commit();
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
