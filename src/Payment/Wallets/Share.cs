using System;
using Payment.Users;

namespace Payment.Wallets
{
    public class Share : IShare
    {
        public Share(IUser owner, string walletId, Money amount)
        {
            Id = Guid.NewGuid().ToString("N");
            WalletId = walletId;
            Owner = owner ?? throw new ArgumentException("Owner must be valid.");
            Amount = amount;
        }

        public string Id { get; }
        public string WalletId { get; }
        public IUser Owner { get; }
        public Money Amount { get; private set; }

        public void AddAmount(Money shareAmount)
        {
            Amount += shareAmount;
        }
    }
}
