using System;
using System.Windows.Input;
using Prism.Navigation;
using Wallet.Core;
using Wallet.Services;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace Wallet.ViewModels
{
    public class PasscodeConfirmationViewModel : ViewModelBase
    {
        readonly NewWalletController controller;
        readonly INavigationService navigator;
        readonly IUserDialogs userDialogs;

        public PasscodeConfirmationViewModel(
            NewWalletController controller,
            INavigationService navigator,
            IUserDialogs userDialogs)
        {
            this.userDialogs = userDialogs;
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

            if (false == verified)
            {
                userDialogs.Toast("Invalid PIN confirmation");

                return;
            }
            await controller.CreateWalletAsync();
            await navigator.NavigateAsync(NavigationKeys.ConnfirmPasscodeOk);
        }
    }
}
