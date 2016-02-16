using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guest.Domain;
using Shared;

namespace Guest.Services.RepositoryContracts
{
    public interface IFriendshipRepository : IRepository<Friendship>
    {
        Friendship Find(string requesterUsername, string responderUsername);
    }
}
