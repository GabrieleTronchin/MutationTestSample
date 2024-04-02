using System.Collections.Generic;

namespace Payment.Core.Contract.Model
{
    public interface IUser
    {
        string Id { get; }
        string Name { get; }
        IList<IUser> Friends { get; }
    }
}