using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace System.Domain
{
    public class SystemManager : IUser, IAggregateRoot
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public Gender Gender { get; set; }
        public string Picture { get; set; }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;
            var other = (SystemManager)obj;
            return Username == other.Username;
        }

        public override int GetHashCode()
        {
            return Username.GetHashCode();
        }
    }
}
