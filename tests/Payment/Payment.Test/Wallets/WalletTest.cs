using NUnit.Framework;
using Payment.Users;
using Payment.Wallets;
using System;

namespace Payment.Test.Wallets;

[TestFixture]

public class WalletTest
{
    [Test]
    public void Initialization_Correct()
    {
        var user = new User("Test");
        var wallet = new Wallet(user, Currency.EUR);
        Assert.False(wallet.Id.Contains("-"));
        Assert.True(Guid.TryParse(wallet.Id, out var _));
    }

    [Test]
    public void Initialization_InvalidOwner()
    {
        var ex = Assert.Throws<ArgumentException>(() => new Wallet(null, Currency.EUR));
        Assert.True(ex.Message == "Owner must be valid.");
    }
}