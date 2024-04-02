namespace Payment.Core.Test
{
    public class MoneyTests
    {
        [Fact]
        public void MoneyEqualityTest()
        {
            var money1 = Money.Euro(50);
            var money2 = Money.Euro(50);
            var money3 = Money.Usd(50);


            Assert.True(money1.Equals(money2));
            Assert.False(money1.Equals(money3));

            Assert.True(money1 == money2);
            Assert.True(money1 != money3);

            //boxing
            Assert.True(money2.Equals((object)money1));

        }

        [Fact]
        public void MoneyAdditionTest()
        {
            var money1 = Money.Euro(50);
            var money2 = Money.Euro(30);
            var money3 = Money.Usd(20);

            var sum1 = money1 + money2;

            Assert.True(Money.Euro(80) == sum1);

            var ex = Assert.Throws<ArgumentException>(() => money1 + money3);
            Assert.True(ex.Message == $"Cannot perform operation with different {nameof(Currency)}");
        }

        [Fact]
        public void MoneySubtractionTest()
        {
            var money1 = Money.Euro(50);
            var money2 = Money.Euro(30);
            var money3 = Money.Usd(20);

            var val = money1 - money2;

            Assert.True(Money.Euro(20) == val);

            var ex = Assert.Throws<ArgumentException>(() => money1 - money3);
            Assert.True(ex.Message == $"Cannot perform operation with different {nameof(Currency)}");
        }


        [Fact]
        public void GetHashCodeTest()
        {
            var money1 = Money.Euro(50);
            var hash = money1.GetHashCode();
            Assert.True(hash == 19850);
        }
    }
}