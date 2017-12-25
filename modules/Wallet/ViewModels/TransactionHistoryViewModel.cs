using System;
using Wallet.Core;
using Wallet.Services;
using Wallet.Models;
using System.Linq;

namespace Wallet.ViewModels
{
    public class TransactionHistoryViewModel : ViewModelBase
    {
        TransactionModel[] _Transactions;
        public TransactionModel[] Transactions
        {
            get => _Transactions;
            set => SetProperty(ref _Transactions, value);
        }

        readonly IAccountsManager accountsManager;

        public TransactionHistoryViewModel(IAccountsManager accountsManager)
        {
            this.accountsManager = accountsManager;
        }

        public override void OnNavigatedTo(Prism.Navigation.NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            LoadData();
        }

        async void LoadData() {
            var receiving = await accountsManager.GetTransactionsAsync();
            var sending = await accountsManager.GetTransactionsAsync(true);

            Transactions = receiving.Union(sending).ToArray();
        }
    }
}
