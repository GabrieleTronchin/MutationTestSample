using Payment.Users;

namespace Payment.Wallets
{
    public interface IShare
    {
        string Id { get; }
        string WalletId { get; }
        IUser Owner { get; }
        Money Amount { get; }
        void AddAmount(Money shareAmount);
    }
}
