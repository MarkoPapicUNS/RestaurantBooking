using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Guest.Domain
{
    public class Guest : IUser, IAggregateRoot
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public Gender Gender { get; set; }
        public string Picture { get; set; }
        public virtual List<Friendship> RequestedFriendships { get; set; }
        public virtual List<Friendship> ReceivedFriendships { get; set; }
    }
}
