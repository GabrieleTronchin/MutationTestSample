using System.Collections.Generic;

namespace Payment.Users
{
    public interface IUser
    {
        string Id { get; }
        string Name { get; }
        IList<IUser> Friends { get; }
    }
}