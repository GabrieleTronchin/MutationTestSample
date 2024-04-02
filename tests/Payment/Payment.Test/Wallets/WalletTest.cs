using Payment.Users;
using Payment.Wallets;

namespace Payment.Test.Wallets
{
    public class WalletTest
    {
        [Fact]
        public void Initialization_Correct()
        {
            var user = new User("Test");
            var wallet = new Wallet(user, Currency.EUR);
            Assert.DoesNotContain("-", wallet.Id);
            Assert.True(Guid.TryParse(wallet.Id, out var _));
        }

        [Fact]
        public void Initialization_InvalidOwner()
        {
            var ex = Assert.Throws<ArgumentException>(() => new Wallet(null, Currency.EUR));
            Assert.True(ex.Message == "Owner must be valid.");
        }
    }
}