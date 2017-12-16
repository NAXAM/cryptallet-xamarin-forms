using System;
using Wallet.Core;
using Wallet.Services;
namespace Wallet.ViewModels
{
    public class WalletViewModel : ViewModelBase
    {
        readonly IAccountsManager accountsManager;

        public WalletViewModel(IAccountsManager accountsManager)
        {
            this.accountsManager = accountsManager;
        }
    }
}
