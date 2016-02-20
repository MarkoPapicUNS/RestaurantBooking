using System;
using System.Collections.Generic;
using System.Domain;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace System.Services.RepositoryContracts
{
    public interface ISystemRepository : IRepository<SystemManager>
    {
        SystemManager Find(string Id);
    }
}
