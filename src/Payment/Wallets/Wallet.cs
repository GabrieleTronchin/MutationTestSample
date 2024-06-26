﻿using System;
using System.Collections.Generic;
using Payment.Users;

namespace Payment.Wallets
{
    public class Wallet : IWallet
    {
        public Wallet(IUser owner, Currency currency)
        {
            Id = Guid.NewGuid().ToString("N");
            Owner = owner ?? throw new ArgumentException("Owner must be valid.");
            Amount = new Money(0, currency);
            Currency = currency;
            Shares = new List<IShare>();
        }

        public string Id { get; }
        public Money Amount { get; private set; }
        public IUser Owner { get; }
        public IList<IShare> Shares { get; }
        public Currency Currency { get; }

        public void AddAmount(Money amount)
        {
            Amount += amount;
        }
    }
}
