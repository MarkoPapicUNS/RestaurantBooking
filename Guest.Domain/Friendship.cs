﻿namespace Guest.Domain
{
    public class Friendship
    {
        public FriendshipStatus Status { get; set; }
        public string RequesterUsername { get; set; }
        public string ResponderUsername { get; set; }

        //for Entity Framework
        public virtual Guest Requester { get; set; }
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
