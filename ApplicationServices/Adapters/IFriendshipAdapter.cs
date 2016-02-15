using System;
using ApplicationServices.Dtos;
using Guest.Domain;

namespace ApplicationServices.Adapters
{
    public interface IFriendshipAdapter
    {
        FriendshipDto AdaptFriendship(Friendship friendship);
    }
}
