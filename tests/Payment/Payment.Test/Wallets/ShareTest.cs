using Payment.Users;
using Payment.Wallets;

namespace Payment.Test.Wallets
{
    public class ShareTest
    {
        [Fact]
        public void Initialization_Correct()
        {
            var user = new User("Test");
            var money = Money.Euro(0);
            var share = new Share(user, "", money);
            Assert.DoesNotContain("-", share.Id);
            Assert.True(Guid.TryParse(share.Id, out var _));
        }

        [Fact]
        public void Initialization_InvalidOwner()
        {
            var money = Money.Euro(0);
            var ex = Assert.Throws<ArgumentException>(() => new Share(null, "", money));
            Assert.True(ex.Message == "Owner must be valid.");
        }


        [Fact]
        public void AddAmounts()
        {
            var user = new User("Test");
            var money = Money.Euro(0);
            var share = new Share(user, "", money);

            share.AddAmount(Money.Euro(5));

            Assert.True(share.Amount.Equals(Money.Euro(5)));
        }

    }
}