using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Adapters;
using ApplicationServices.Dtos;
using Guest.Domain;
using Guest.Services.Exceptions;

namespace ApplicationServices
{
    public class FriendshipService : IFriendshipService
    {
        private Guest.Services.IFriendshipService _friendshipService;
        private IFriendshipAdapter _adapter;

        public FriendshipService(Guest.Services.IFriendshipService friendshipService, IFriendshipAdapter adapter)
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

        public FriendshipDto GetFriendship(string requesterUsername, string responderUsername)
        {
            FriendshipDto friendshipDto;
            try
            {
                var friendship = _friendshipService.GetFriendship(requesterUsername, responderUsername);
                friendshipDto = _adapter.AdaptFriendship(friendship);
            }
            catch (FriendshipException fe)
            {
                friendshipDto = null;
            }
            return friendshipDto;
        }
    }
}
