using System.Collections.Generic;

namespace Payment.Wallets
{
    public static class WalletMemoryRepository
    {
        private static Dictionary<string, Wallet> _wallets = new Dictionary<string, Wallet>();

        public static Wallet FindWallet(string id)
        {
            if (!_wallets.TryGetValue(id, out var wallet))
                return null;

            return wallet;
        }

        public static void Save(Wallet wallet)
        {
            _wallets.Add(wallet.Id, wallet);
        }
    }
}