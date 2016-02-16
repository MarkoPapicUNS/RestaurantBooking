using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Dtos;

namespace ApplicationServices
{
    public interface IGuestAppService
    {
        IGuestDto GetGuest(string username, string guestUsername);
        IEnumerable<FriendDisplayDto> GetGuests(string username);
    }
}
