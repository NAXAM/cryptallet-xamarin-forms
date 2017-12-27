using System;
using Wallet.Core;
using Wallet.Services;
using Wallet.Models;
using System.Linq;

namespace Wallet.ViewModels
{
    public class TransactionHistoryViewModel : ViewModelBase
    {
        bool _IsFetching;
        public bool IsFetching
        {
            get => _IsFetching;
            set => SetProperty(ref _IsFetching, value);
        }

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
            IsFetching = true;

            var receiving = await accountsManager.GetTransactionsAsync();
            var sending = await accountsManager.GetTransactionsAsync(true);

            Transactions = receiving.Union(sending).OrderByDescending(x => x.Timestamp).ToArray();

            IsFetching = false;
        }
    }
}
