using Payment.Users;

namespace Payment.Wallets
{
    public interface IWalletService
    {
        /// <summary>
        /// Create a wallet
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        IWallet CreateWallet(IUser owner, Currency currency);

        /// <summary>
        /// Get a wallet by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IWallet GetWallet(string id);

        /// <summary>
        /// Add Amount to a wallet
        /// - Adds amount into the wallet amount
        /// - If there's no share from this author in the wallet,  Creates a new Share of this amount from the author
        /// - If there's already a share from the author , adds the amount to the existing share
        /// </summary>
        /// <param name="wallet"></param>
        /// <param name="author"></param>
        /// <param name="amount"></param>
        void Contribute(IWallet wallet, IUser author, Money amount);
    }
}
