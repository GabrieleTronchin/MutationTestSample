using NUnit.Framework;
using Payment.Core;
using Payment.Core.Contract.Service;
using Payment.Users;
using Payment.Wallets;
using System;
using System.Linq;

namespace Payment.Test.Wallets
{
    [Category("Step 2: Wallet Service")]
    [TestFixture]
    public class Step3WalletServiceTests
    {
        [Test]
        public void Test_CreateWallet()
        {
            IWalletService sut = new WalletService();
            var owner = new User("John Doe");
            var createdWallet = sut.CreateWallet(owner, Currency.USD);
            Assert.NotNull(createdWallet);
            Assert.AreEqual(0, createdWallet.Amount.Value);
            Assert.AreEqual(Currency.USD, createdWallet.Amount.Currency, "Wallet currency should be USD");
            Assert.AreEqual(owner, createdWallet.Owner);
            Assert.IsEmpty(createdWallet.Shares);
        }

        [Test]
        public void Test_GetWallet()
        {
            IWalletService sut = new WalletService();
            var owner = new User(" John Doe");
            var createdWallet = sut.CreateWallet(owner, Currency.USD);

            var wallet = sut.GetWallet(createdWallet.Id);
            Assert.AreEqual(createdWallet, wallet);
        }

        [Test]
        public void Test_GetWallet_Should_Return_Null_For_Unknown_Wallet()
        {
            IWalletService sut = new WalletService();
            var wallet = sut.GetWallet("fakeWalletId");
            Assert.IsNull(wallet);
        }

        [Test]
        public void Test_ContributeToWallet()
        {
            IWalletService sut = new WalletService();
            var owner = new User(" John Doe");
            var contributor = new User(" François Dupont");
            var createdWallet = sut.CreateWallet(owner, Currency.USD);
            var amount = new Money(50, Currency.USD);

            sut.Contribute(createdWallet, contributor, amount);
            var wallet = sut.GetWallet(createdWallet.Id);

            Assert.AreEqual(wallet.Amount.Value, 50);
            Assert.AreEqual(wallet.Shares.Count, 1);
            Assert.AreEqual(wallet.Shares.First().Owner.Id, contributor.Id);
            Assert.AreEqual(wallet.Shares.First().Amount.Value, amount.Value);
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
        public void Test_ContributeToWalletNegativeValue()
        {
            IWalletService sut = new WalletService();
            var owner = new User(" John Doe");
            var contributor = new User(" François Dupont");
            var createdWallet = sut.CreateWallet(owner, Currency.USD);
            var amount = new Money(-50, Currency.USD);

            Assert.Throws<ArgumentException>(() => sut.Contribute(createdWallet, contributor, amount), "User should not be able to contribute with a negative value");
        }

        [Test]
        public void Test_Contribute_Should_Have_Correct_Shares()
        {
            IWalletService sut = new WalletService();
            var owner = new User(" John Doe");
            var contributor = new User(" François Dupont");
            var createdWallet = sut.CreateWallet(owner, Currency.EUR);
            var amount = Money.Euro(10);

            sut.Contribute(createdWallet, contributor, amount);

            Assert.AreEqual(1, createdWallet.Shares.Count);
            Assert.AreEqual(amount, createdWallet.Shares.Single().Amount);
            Assert.AreEqual(contributor.Id, createdWallet.Shares.Single().Owner.Id);

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
    }
}
