using System;
using System.Collections.Generic;
using ApplicationServices.Dtos;
using Guest.Domain;

namespace ApplicationServices.Adapters
{
    public interface IFriendshipAdapter
    {
        FriendshipDto AdaptFriendship(Friendship friendship);
        FriendDto AdaptFriend(Guest.Domain.Guest friend);
    }
}
