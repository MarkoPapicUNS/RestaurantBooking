using System;
using System.Linq;
using Guest.Domain;
using Guest.Services.Exceptions;
using Guest.Services.RepositoryContracts;
using System.Threading.Tasks;
using Shared;
using Guest = Guest.Domain.Guest;

namespace Guest.Services
{
    public class GuestService : IGuestService
    {
        private IGuestRepository _repository;
        private ILogger _logger;

        public GuestService(IGuestRepository repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IQueryable<Domain.Guest> GetGuests(string username)
        {
			//Task.Run(() => _logger.Log(LogMessageType.Notification, "Started retrieving all guests"));
	        _logger.Log(LogMessageType.Notification, "Started retrieving all guests");
			var guests = _repository.All().Where(g => g.Username != username);
			return guests;
        }

        public Domain.Guest GetGuest(string username)
        {
			//Task.Run(() => _logger.Log(LogMessageType.Notification, string.Format("Started retrieving guest {0}", username)));
	        _logger.Log(LogMessageType.Notification, string.Format("Started retrieving guest {0}", username));
			return _repository.Find(username);
        }

        public void AddGuest(string username)
        {
            _logger.Log(LogMessageType.Notification, string.Format("Adding guest {0}", username));
            var guest = _repository.Find(username);
            if (guest != null)
                throw new GuestException(string.Format("Guest with username {0} already exists", username));
            _repository.Insert(new Domain.Guest
            {
                Username = username,
                DisplayFullName = false,
                Address = new Address()
            });
            _repository.Commit();
        }

        public void UpdateProfile(Domain.Guest profileData)
	    {
			if (profileData == null)
				throw new ArgumentNullException("profileData");

			//Task.Run(() => _logger.Log(LogMessageType.Notification, string.Format("Started updating profile for {0}", profileData.Username)));
		    _logger.Log(LogMessageType.Notification, string.Format("Started updating profile for {0}", profileData.Username));
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
			//Task.Run(() => _logger.Log(LogMessageType.Notification, string.Format("Started retrieving all frends for {0}", username)));
		    _logger.Log(LogMessageType.Notification, string.Format("Started retrieving all frends for {0}", username));
			var guest = _repository.Find(username);
            var requesterUsernames = guest.ReceivedFriendships.Where(rf => rf.Status == FriendshipStatus.Active).Select(rf => rf.RequesterUsername);
            var responderUsernames = guest.RequestedFriendships.Where(rf => rf.Status == FriendshipStatus.Active).Select(rf => rf.ResponderUsername);
            var friendUsernames = requesterUsernames.Union(responderUsernames);
            var friends = _repository.All().Where(f => friendUsernames.Contains(f.Username));
            return friends;
        }

        public IQueryable<Domain.Guest> GetSentFriendRequests(string username)
        {
            if (username == null)
                throw new ArgumentNullException("username");

			//Task.Run(() => _logger.Log(LogMessageType.Notification, string.Format("Started retrieving all frend requests for {0}", username)));
	        _logger.Log(LogMessageType.Notification,
		        string.Format("Started retrieving all frend requests for {0}", username));
			var guest = _repository.Find(username);
            var usernames = guest.RequestedFriendships.Where(rf => rf.Status == FriendshipStatus.RequestPending).Select(rf => rf.ResponderUsername);
            return _repository.All().Where(g => usernames.Contains(g.Username));

        }

        public IQueryable<Domain.Guest> GetFriendRequests(string username)
        {
            if (username == null)
                throw new ArgumentNullException("username");

			//Task.Run(() => _logger.Log(LogMessageType.Notification, string.Format("Started retrieving all sent frend requests for {0}", username)));
	        _logger.Log(LogMessageType.Notification,
		        string.Format("Started retrieving all sent frend requests for {0}", username));
			var guest = _repository.Find(username);
            var usernames = guest.ReceivedFriendships.Where(rf => rf.Status == FriendshipStatus.RequestPending).Select(rf => rf.RequesterUsername);
            return _repository.All().Where(g => usernames.Contains(g.Username));
        }
    }
}
