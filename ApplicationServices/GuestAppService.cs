using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Adapters;
using ApplicationServices.Dtos;
using ApplicationServices.Models;
using Guest.Services;
using Guest.Services.Exceptions;
using Shared;

namespace ApplicationServices
{
    public class GuestAppService : IGuestAppService
    {
        private IGuestService _guestService;
        private IGuestAdapter _adapter;
        private ILogger _logger;
        
        public GuestAppService(IGuestService guestService, IGuestAdapter adapter, ILogger logger)
        {
            _guestService = guestService;
            _adapter = adapter;
            _logger = logger;
        }

        public IGuestDto GetGuest(string username, string guestUsername)
        {
            //var me = _guestService.GetGuest(username); I decided to trust controller
            var guest = _guestService.GetGuest(guestUsername);
            if (guest == null)
                return null;
            var guestFriends = _guestService.GetFriends(guestUsername);
			var friendRequests = _guestService.GetFriendRequests(guestUsername);
			var sentFriendRequests = _guestService.GetSentFriendRequests(guestUsername);
			if (username == guest.Username)
            {
//                var friendRequests = _guestService.GetFriendRequests(guestUsername);
//                var sentFriendRequests = _guestService.GetSentFriendRequests(guestUsername);
                return _adapter.AdaptMeGuest(guest, guestFriends, friendRequests, sentFriendRequests);
            }
            else if (guestFriends.Any(gf => gf.Username == username))
                return _adapter.AdaptFriendGuest(guest, guestFriends);
			else if (sentFriendRequests.Any(fr => fr.Username == username))
				return _adapter.AdaptStrangerGuest(guest, GuestRelation.RequestReceived);
			else if (friendRequests.Any(fr => fr.Username == username))
				return _adapter.AdaptStrangerGuest(guest, GuestRelation.RequestSent);
            else
                return _adapter.AdaptStrangerGuest(guest, GuestRelation.Stranger);
        }

        public IEnumerable<FriendDisplayDto> GetGuests(string username)
        {
            var guests = _guestService.GetGuests(username).ToArray();
            return guests.Select(g => _adapter.AdaptGuestDisplay(g));
        }

        public ActionResultDto AddGuest(string username)
        {
            var result = new ActionResultDto();
            try
            {
                _guestService.AddGuest(username);
                result.IsSuccess = true;
                result.Message = string.Format("Guest {0} succesfully registered!", username);
                _logger.Log(LogMessageType.Notification, string.Format("Guest {0} successfully registered!", username));
            }
            catch (GuestException ge)
            {
                result.IsSuccess = false;
                result.Message = ge.Message;
                _logger.Log(LogMessageType.Error, ge.Message);
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Message = "Unable to process request";
                _logger.Log(LogMessageType.Error, e.Message);
            }
            return result;
        }

        public ActionResultDto UpdateProfile(ProfileModel profileModel)
	    {
			var result = new ActionResultDto();
		    var guest = _adapter.CreateGuestFromProfileModel(profileModel);
		    try
		    {
			    _guestService.UpdateProfile(guest);
			    result.IsSuccess = true;
			    result.Message = "Profile succesfully updated!";
				//Task.Run(() => _logger.Log(LogMessageType.Notification, string.Format("Succesfully update profile for {0}", profileModel.Username)));
			    _logger.Log(LogMessageType.Notification,
				    string.Format("Succesfully update profile for {0}", profileModel.Username));

		    }
		    catch (GuestException ge)
		    {
			    result.IsSuccess = false;
			    result.Message = ge.Message;
            }
			return result;
	    }

        public IEnumerable<FriendDisplayDto> GetFriends(string username)
        {
            IEnumerable<FriendDisplayDto> friendsDtos;
            try
            {
                var friends = _guestService.GetFriends(username).ToArray();
                friendsDtos = friends.Select(f => _adapter.AdaptGuestDisplay(f));
            }
            catch (Exception e)
            {

                friendsDtos = null;
            }
            return friendsDtos;
        }
    }
}
