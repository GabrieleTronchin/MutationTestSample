using NUnit.Framework;
using System;

namespace Payment.Test
{
    [TestFixture]

    public class MoneyTests
    {
        [Test]
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

        [Test]
        public void MoneyAdditionTest()
        {
            var money1 = Money.Euro(50);
            var money2 = Money.Euro(30);
            var money3 = Money.Usd(20);

            var sum1 = money1 + money2;

            Assert.True(Money.Euro(80) == sum1);

            Assert.That(() => money1 + money3, Throws.ArgumentException);
            //Assert.True(ex.Message == $"Cannot perform operation with different {nameof(Currency)}");
        }

        [Test]
        public void MoneySubtractionTest()
        {
            var money1 = Money.Euro(50);
            var money2 = Money.Euro(30);
            var money3 = Money.Usd(20);

            var val = money1 - money2;

            Assert.True(Money.Euro(20) == val);


            Assert.That(() => money1 - money3, Throws.ArgumentException);

            //Assert.True(ex.Message == $"Cannot perform operation with different {nameof(Currency)}");
        }


        [Test]
        public void GetHashCodeTest()
        {
            var money1 = Money.Euro(50);
            var hash = money1.GetHashCode();
            Assert.True(hash == 19850);
        }
    }
}