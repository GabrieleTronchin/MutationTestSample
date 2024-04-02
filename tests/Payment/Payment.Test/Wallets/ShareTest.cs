using NUnit.Framework;
using Payment.Users;
using Payment.Wallets;
using System;

namespace Payment.Test.Wallets;

[TestFixture]

public class ShareTest
{
    [Test]
    public void Initialization_Correct()
    {
        var user = new User("Test");
        var money = Money.Euro(0);
        var share = new Share(user, "", money);
        Assert.False(share.Id.Contains("-"));
      Assert.True(Guid.TryParse(share.Id, out var _));
    }

    [Test]
    public void Initialization_InvalidOwner()
    {
        var money = Money.Euro(0);
        var ex = Assert.Throws<ArgumentException>(() => new Share(null, "", money));
        Assert.True(ex.Message == "Owner must be valid.");
    }


    [Test]
    public void AddAmounts()
    {
        var user = new User("Test");
        var money = Money.Euro(0);
        var share = new Share(user, "", money);

        share.AddAmount(Money.Euro(5));

        Assert.True(share.Amount.Equals(Money.Euro(5)));
    }

}