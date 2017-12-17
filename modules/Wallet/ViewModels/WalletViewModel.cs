using System;
using System.Windows.Input;
using Wallet.Core;
using Wallet.Services;
using Xamarin.Forms;

namespace Wallet.ViewModels
{
    public class WalletViewModel : ViewModelBase
    {
        decimal _Balance;
        public decimal Balance
        {
            get => _Balance;
            set => SetProperty(ref _Balance, value);
        }

        public string DefaultAccountAddress => accountsManager.DefaultAccountAddress;

        string _RecipientAddress;
        public string RecipientAddress
        {
            get => _RecipientAddress;
            set => SetProperty(ref _RecipientAddress, value);
        }

        decimal _SendingAmount;
        public decimal SendingAmount
        {
            get => _SendingAmount;
            set => SetProperty(ref _SendingAmount, value);
        }

        readonly IAccountsManager accountsManager;

        public WalletViewModel(IAccountsManager accountsManager)
        {
            this.accountsManager = accountsManager;
        }

        public async override void Load(object data)
        {
            base.Load(data);

            Balance = await accountsManager.GetBalanceInBQCAsync(accountsManager.DefaultAccountAddress);
        }

        ICommand _SendCommand;
        public ICommand SendCommand
        {
            get { return (_SendCommand = _SendCommand ?? new Command<object>(ExecuteSendCommand, CanExecuteSendCommand)); }
        }
        bool CanExecuteSendCommand(object obj) => true;
        void ExecuteSendCommand(object obj) { }
    }
}
