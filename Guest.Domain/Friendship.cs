namespace Guest.Domain
{
    public class Friendship
    {
        public FriendshipStatus Status { get; set; }
        public string RequesterUsername { get; set; }
        public string ResponderUsername { get; set; }

        //for Entity Framework
        public virtual Guest Requester { get; set; }
        public virtual Guest Responder { get; set; }
    }
}
