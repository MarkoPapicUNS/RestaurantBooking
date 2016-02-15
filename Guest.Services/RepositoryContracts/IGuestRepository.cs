using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guest.Domain;
using Shared;

namespace Guest.Services.RepositoryContracts
{
    public interface IGuestRepository : IRepository<Domain.Guest, string>
    {
        Friendship GetFriendship(string requesterUsername, string responderUsername);
        IQueryable<Friendship> GetFriendships();
    }
}
