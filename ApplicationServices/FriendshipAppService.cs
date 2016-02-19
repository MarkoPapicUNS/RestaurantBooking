using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Adapters;
using ApplicationServices.Dtos;
using Guest.Domain;
using Guest.Services;
using Guest.Services.Exceptions;
using Shared;

namespace ApplicationServices
{
    public class FriendshipAppService : IFriendshipAppService
    {
        private IFriendshipService _friendshipService;
        private ILogger _logger;

        public FriendshipAppService(IFriendshipService friendshipService, ILogger logger)
        {
            _friendshipService = friendshipService;
            _logger = logger;
        }

        public ActionResultDto SendFriendRequest(string senderUsername, string receiverUsername)
        {
            ActionResultDto resultDto;
            try
            {
                _friendshipService.SendRequest(senderUsername, receiverUsername);
                resultDto = new ActionResultDto
                {
                    IsSuccess = true,
                    Message = string.Format("Friend request succesfully sent to {0}.", receiverUsername)
                };
				//Task.Run(() => _logger.Log(LogMessageType.Notification, string.Format("Friend request successfully sent from {0} to {1}", senderUsername, receiverUsername)));
	            _logger.Log(LogMessageType.Notification,
		            string.Format("Friend request successfully sent from {0} to {1}", senderUsername, receiverUsername));

            }
            catch (FriendshipException fe)
            {
                resultDto = new ActionResultDto
                {
                    IsSuccess = false,
                    Message = fe.Message
                };
            }
            catch (Exception e)
            {
				//Task.Run(() => _logger.Log(LogMessageType.Notification, e.Message));
	            _logger.Log(LogMessageType.Notification, e.Message);

				resultDto = new ActionResultDto
                {
                    IsSuccess = false,
                    Message = "Request cannot be processed"
                };
            }
            return resultDto;
        }

        public string GetFriendRequest(string senderUsername, string recipientUsername)
        {
            var friendship = _friendshipService.GetFriendRequest(senderUsername, recipientUsername);
            return friendship == null ? null : string.Format("{0} to {1}", friendship.ResponderUsername, friendship.ResponderUsername);
        }

        /*public IEnumerable<FriendshipDto> GetFriendRequests(string recipientUsername)
        {
            var friendships = _friendshipService.GetFriendRequests(recipientUsername);
            return friendships == null ? null : friendships.Select(f => _adapter.AdaptFriendship(f));
        }

        public IEnumerable<FriendshipDto> GetSentFriendRequests(string senderUsername)
        {
            var friendships = _friendshipService.GetSentFriendRequests(senderUsername);
            return friendships == null ? null : friendships.Select(f => _adapter.AdaptFriendship(f));
        }*/

        public ActionResultDto RemoveFriendship(string username, string friendUsername)
        {
            ActionResultDto result;
            try
            {
                _friendshipService.RemoveFriendship(username, friendUsername);
                result = new ActionResultDto
                {
                    IsSuccess = true,
                    Message = string.Format("{0} is removed friends.", friendUsername)
                };
				//Task.Run(() => _logger.Log(LogMessageType.Notification, string.Format("{0} removed successfully from {1}'s friends", friendUsername, username)));
	            _logger.Log(LogMessageType.Notification,
		            string.Format("{0} removed successfully from {1}'s friends", friendUsername, username));

            }
            catch (Exception e)
            {
				//Task.Run(() => _logger.Log(LogMessageType.Notification, e.Message));
	            _logger.Log(LogMessageType.Notification, e.Message);
				result = new ActionResultDto
                {
                    IsSuccess = false,
                    Message = "Unable to perform this request."
                };
            }
            return result;
        }
    }
}
