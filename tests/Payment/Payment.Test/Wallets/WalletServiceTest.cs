using NUnit.Framework;
using Payment.Users;
using Payment.Wallets;
using System;

namespace Payment.Test.Wallets;

[TestFixture]
public class WalletServiceTest
{
    [Test]
    public void Contribute_InvalidCurrency()
    {
        var user = new User("Test");
        var wallet = new Wallet(user, Currency.EUR);

        var walletService = new WalletService();

        var ex = Assert.Throws<ArgumentException>(() => walletService.Contribute(wallet, user, Money.Usd(50)));

        Assert.AreEqual("User should not be able to contribute in a different currency than the wallet one", ex.Message);

    }

    [Test]
    public void Contribute_InvalidAmounts()
    {
        var user = new User("Test");
        var wallet = new Wallet(user, Currency.EUR);

        var walletService = new WalletService();

        var ex = Assert.Throws<ArgumentException>(() => walletService.Contribute(wallet, user, Money.Euro(-50)));

        Assert.AreEqual("User should not be able to contribute with a negative value", ex.Message);

    }

}