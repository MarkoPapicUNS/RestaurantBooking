using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guest.Domain;

namespace Guest.Services
{
    public interface IGuestService
    {
        IQueryable<Domain.Guest> GetGuests();
        IQueryable<Domain.Guest> GetFriends(string username);
        IEnumerable<Friendship> GetFriendRequests(string username);
        IEnumerable<Friendship> GetSentFriendRequests(string username);
    }
}
