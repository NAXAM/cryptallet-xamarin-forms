using System;
using Wallet.Core;
using Wallet.Services;
namespace Wallet.ViewModels
{
    public class PasscodeConfirmationViewModel : ViewModelBase
    {
        readonly NewWalletController controller;

        public PasscodeConfirmationViewModel(NewWalletController controller)
        {
            this.controller = controller;
        }
    }
}
