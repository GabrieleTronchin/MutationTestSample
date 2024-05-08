using System;
using System.Linq;
using Payment.Users;

namespace Payment.Wallets
{
    public class WalletService : IWalletService
    {
        public IWallet CreateWallet(IUser owner, Currency currency)
        {
            var wallet = new Wallet(owner, currency);
            WalletMemoryRepository.Save(wallet);
            return wallet;
        }

        public IWallet GetWallet(string id)
        {
            return WalletMemoryRepository.FindWallet(id);
        }

        public void Contribute(IWallet wallet, IUser author, Money amount)
        {
            if (wallet.Currency != amount.Currency)
                throw new ArgumentException(
                    "User should not be able to contribute in a different currency than the wallet one"
                );

            if (amount.Value <= 0)
                throw new ArgumentException(
                    "User should not be able to contribute with a negative value"
                );

            wallet.AddAmount(amount);

            var contributorShare = wallet.Shares.SingleOrDefault(x => x.Owner.Id == author.Id);

            if (contributorShare == null)
            {
                wallet.Shares.Add(new Share(author, wallet.Id, amount));
            }
            else
            {
                contributorShare.AddAmount(amount);
            }
        }
    }
}
