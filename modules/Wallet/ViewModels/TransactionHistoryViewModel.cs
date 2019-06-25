/*
 * Copyright 2018 NAXAM CO.,LTD.
 *
 *   Licensed under the Apache License, Version 2.0 (the "License");
 *   you may not use this file except in compliance with the License.
 *   You may obtain a copy of the License at
 *
 *       http://www.apache.org/licenses/LICENSE-2.0
 *
 *   Unless required by applicable law or agreed to in writing, software
 *   distributed under the License is distributed on an "AS IS" BASIS,
 *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *   See the License for the specific language governing permissions and
 *   limitations under the License.
 */ 
using System;
using Wallet.Core;
using Wallet.Services;
using Wallet.Models;
using System.Linq;
using Prism.Navigation;

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

        public override void OnNavigatedTo(INavigationParameters parameters)
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
