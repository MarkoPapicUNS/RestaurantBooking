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

namespace ApplicationServices
{
    public class GuestAppService : IGuestAppService
    {
        private IGuestService _guestService;
        private IGuestAdapter _adapter;


        public GuestAppService(IGuestService guestService, IGuestAdapter adapter)
        {
            _guestService = guestService;
            _adapter = adapter;
        }

        public IGuestDto GetGuest(string username, string guestUsername)
        {
            //var me = _guestService.GetGuest(username); I decided to trust controller
            var guest = _guestService.GetGuest(guestUsername);
            if (guest == null)
                return null;
            var guestFriends = _guestService.GetFriends(guestUsername);
            if (username == guest.Username)
            {
                var friendRequests = _guestService.GetFriendRequests(guestUsername);
                var sentFriendRequests = _guestService.GetSentFriendRequests(guestUsername);
                return _adapter.AdaptMeGuest(guest, guestFriends, friendRequests, sentFriendRequests);
            }
            else if (guestFriends.Any(gf => gf.Username == username))
                return _adapter.AdaptFriendGuest(guest, guestFriends);
            else
                return _adapter.AdaptStrangerGuest(guest);
        }

        public IEnumerable<FriendDisplayDto> GetGuests(string username)
        {
            var guests = _guestService.GetGuests(username).ToArray();
            return guests.Select(g => _adapter.AdaptGuestDisplay(g));
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
		    }
		    catch (GuestException ge)
		    {
			    result.IsSuccess = false;
			    result.Message = ge.Message;
		    }
			return result;
	    }
    }
}
