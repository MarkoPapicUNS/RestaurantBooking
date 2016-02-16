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
        public FriendRequestDto AdaptFriendship(Friendship friendship)
        {
            if (friendship == null)
                throw new ArgumentNullException("friendship");

            var friendshipDto = new FriendRequestDto
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
                DisplayName = friend.DisplayFullName ? string.Format("{0} {1}", friend.FirstName, friend.LastName) : friend.Username,
                Picture = friend.Picture
            };
            return friendDto;
        }
    }
}
