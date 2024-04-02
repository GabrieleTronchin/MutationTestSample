using NUnit.Framework;
using Payment.Users;
using System;

namespace Payment.Test.Users;

[TestFixture]
public class UserTest
{
    [Test]
    public void Initialization_Correct()
    {
        var user = new User("Test");
        Assert.False(user.Id.Contains("-"));
        Assert.True(Guid.TryParse(user.Id, out var _));
    }
}