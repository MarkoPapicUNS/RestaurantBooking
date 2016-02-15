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
    }
}
