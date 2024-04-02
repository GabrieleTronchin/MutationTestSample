using NUnit.Framework;
using Payment.Users;
using Payment.Wallets;
using System;
using System.Linq;

namespace Payment.Test.Wallets;

[TestFixture]

public class WalletTestForKillMutators
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
    public void Initialization_InvalidWallet()
    {
        var ex = Assert.Throws<ArgumentException>(() => new Wallet(null, Currency.EUR));
        Assert.True(ex.Message == "Owner must be valid.");
    }


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

    [Test]
    public void Test_ContributeToWalletDifferentCurrency()
    {
        IWalletService sut = new WalletService();
        var owner = new User(" John Doe");
        var contributor = new User(" François Dupont");
        var createdWallet = sut.CreateWallet(owner, Currency.USD);
        var amount = new Money(50, Currency.EUR);

        Assert.Throws<ArgumentException>(() => sut.Contribute(createdWallet, contributor, amount), "User should not be able to contribute in a different currency than the wallet one");
    }


    [Test]
    public void Test_Contribute_Should_Have_Correct_Shares_For_Non_Empty_Wallet()
    {
        IWalletService sut = new WalletService();
        var owner = new User("John Doe");
        var contributor1 = new User("François Dupont");
        var wallet = sut.CreateWallet(owner, Currency.EUR);
        sut.Contribute(wallet, contributor1, Money.Euro(10));

        var contributor2 = new User("Sara MonaLisa");
        sut.Contribute(wallet, contributor2, Money.Euro(50));
        sut.Contribute(wallet, contributor1, Money.Euro(100));

        Assert.AreEqual(2, wallet.Shares.Count, "Wallet should have two shares");
        Assert.AreEqual(Money.Euro(110), wallet.Shares.Single(sh => sh.Owner.Id == contributor1.Id).Amount, "contributor1 should have a share of 110");
        Assert.AreEqual(Money.Euro(50), wallet.Shares.Single(sh => sh.Owner.Id == contributor2.Id).Amount, "contributor2 should have a share of 50");
    }

    [Test]
    public void Initialization_InvalidShare()
    {
        var money = Money.Euro(0);
        var ex = Assert.Throws<ArgumentException>(() => new Share(null, "", money));
        Assert.True(ex.Message == "Owner must be valid.");
    }
}