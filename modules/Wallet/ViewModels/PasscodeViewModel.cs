using System;
using System.Windows.Input;
using Wallet.Core;
using Wallet.Services;
using Xamarin.Forms;

namespace Wallet.ViewModels
{
    public class PasscodeViewModel : ViewModelBase
    {
        readonly NewWalletController controller;
        readonly INavigationService navigator;

        public PasscodeViewModel(
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
            controller.SetPasscode(passcode);
            await navigator.NavigateAsync(NavigationKeys.ConfirmPasscode);
        }
    }
}
