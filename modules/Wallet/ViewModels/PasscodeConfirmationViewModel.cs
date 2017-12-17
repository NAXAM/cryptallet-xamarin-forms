using System;
using System.Windows.Input;
using Wallet.Core;
using Wallet.Services;
using Xamarin.Forms;

namespace Wallet.ViewModels
{
    public class PasscodeConfirmationViewModel : ViewModelBase
    {
        readonly NewWalletController controller;
        readonly INavigationService navigator;

        public PasscodeConfirmationViewModel(
            NewWalletController controller,
            INavigationService navigator)
        {
            this.controller = controller;
            this.navigator = navigator;
        }

        ICommand _ContinueCommand;
        public ICommand ContinueCommand
        {
            get { return (_ContinueCommand = _ContinueCommand ?? new Command<string>(ExecuteContinueCommand, CanExecuteContinueCommand)); }
        }
        bool CanExecuteContinueCommand(string obj) => true;
        async void ExecuteContinueCommand(string passcode)
        {
            var verified = controller.VerifyPasscode(passcode);

            if (false == verified) return;

            await controller.CreateWallet();
            await navigator.NavigateAsync(NavigationKeys.ConnfirmPasscodeOk);
        }
    }
}
