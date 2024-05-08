using System;
using System.Collections.Generic;

namespace Payment.Users
{
    public class User : IUser
    {
        public User(string name)
        {
            Id = Guid.NewGuid().ToString("N");
            Name = name;
            Friends = new List<IUser>();
        }

        public string Id { get; }
        public string Name { get; }
        public IList<IUser> Friends { get; }
    }
}
