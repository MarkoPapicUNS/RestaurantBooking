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

namespace ApplicationServices
{
    public class FriendshipAppService : IFriendshipAppService
    {
        private IFriendshipService _friendshipService;
        private IFriendshipAdapter _adapter;

        public FriendshipAppService(IFriendshipService friendshipService, IFriendshipAdapter adapter)
        {
            _friendshipService = friendshipService;
            _adapter = adapter;
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
                resultDto = new ActionResultDto
                {
                    IsSuccess = false,
                    Message = "Request cannot be processed"
                };
            }
            return resultDto;
        }

        public FriendRequestDto GetFriendRequest(string senderUsername, string recipientUsername)
        {
            var friendship = _friendshipService.GetFriendRequest(senderUsername, recipientUsername);
            return friendship == null ? null : _adapter.AdaptFriendship(friendship);
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
            }
            catch (Exception e)
            {
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
