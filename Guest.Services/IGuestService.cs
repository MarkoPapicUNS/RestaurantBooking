using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guest.Services
{
    public interface IGuestService
    {
        IQueryable<Domain.Guest> GetGuests(string username);
        Domain.Guest GetGuest(string username);
        IQueryable<Domain.Guest> GetFriends(string username);
        IQueryable<Domain.Guest> GetFriendRequests(string username);
        IQueryable<Domain.Guest> GetSentFriendRequests(string username);
    }
}
