using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Dtos;
using Guest.Domain;

namespace ApplicationServices.Adapters
{
    public class FriendshipAdapter : IFriendshipAdapter
    {
        public FriendshipDto AdaptFriendship(Friendship friendship)
        {
            if (friendship == null)
                throw new ArgumentNullException("friendship");

            var friendshipDto = new FriendshipDto
            {
                RequesterUsername = friendship.RequesterUsername,
                ResponderUsername = friendship.ResponderUsername
            };
            return friendshipDto;
        }

        public FriendDto AdaptFriend(Guest.Domain.Guest friend)
        {
            if (friend == null)
                throw new ArgumentNullException("friend");

            var friendDto = new FriendDto
            {
                Username = friend.Username,
                Picture = friend.Picture
            };
            return friendDto;
        }
    }
}
