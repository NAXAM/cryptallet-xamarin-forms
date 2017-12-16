using System;
using Wallet.Core;
using Wallet.Services;
namespace Wallet.ViewModels
{
    public class PassphraseConfirmationViewModel : ViewModelBase
    {
        readonly NewWalletController controller;
        readonly INavigationService navigator;

        public PassphraseConfirmationViewModel(
            NewWalletController controller,
            INavigationService navigator)
        {
            this.controller = controller;
            this.navigator = navigator;
        }
    }
}
