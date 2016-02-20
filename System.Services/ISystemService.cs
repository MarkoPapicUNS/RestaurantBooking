using System;
using System.Collections.Generic;
using System.Domain;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Services
{
    public interface ISystemService
    {
        void AddSystemManager(string username);
        void RemoveSystemManager(string username);
        SystemManager GetSystemManager(string username);
        IEnumerable<SystemManager> GetSystemManagers();
    }
}
