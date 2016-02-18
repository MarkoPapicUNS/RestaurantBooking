using Newtonsoft.Json;
using Shared;

namespace Guest.Domain
{
    public class Friendship : IAggregateRoot
    {
        public FriendshipStatus Status { get; set; }
        public string RequesterUsername { get; set; }
        public string ResponderUsername { get; set; }

        //for Entity Framework
		[JsonIgnore]
        public virtual Guest Requester { get; set; }
		[JsonIgnore]
        public virtual Guest Responder { get; set; }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;
            var other = (Friendship)obj;
            return RequesterUsername == other.RequesterUsername && ResponderUsername == other.ResponderUsername;
        }

        public override int GetHashCode()
        {
            return (RequesterUsername + ResponderUsername).GetHashCode();
        }
    }
}
