using Payment.Users;
using System.Collections.Generic;

namespace Payment.Wallets
{
    public interface IWallet
    {
        string Id { get; }
        Money Amount { get; }
        Currency Currency { get; }
        IUser Owner { get; }
        IList<IShare> Shares { get; }
        void AddAmount(Money amount);
    }
}