namespace Shared
{
    public interface IUser
    {
        string Username { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        Address Address { get; set; }
        Gender Gender { get; set; }
        string Picture { get; set; }
    }
}
