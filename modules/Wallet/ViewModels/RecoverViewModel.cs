using System.Windows.Input;
using Prism.Navigation;
using Wallet.Core;
using Wallet.Services;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace Wallet.ViewModels
{
    public class RecoverViewModel : ViewModelBase
    {
        string _Words;
        public string Words
        {
            get => _Words;
            set => SetProperty(ref _Words, value);
        }

        string _Passcode;
        public string Passcode
        {
            get => _Passcode;
            set => SetProperty(ref _Passcode, value);
        }

        string _PasscodeConfirmation;
        public string PasscodeConfirmation
        {
            get => _PasscodeConfirmation;
            set => SetProperty(ref _PasscodeConfirmation, value);
        }

        readonly INavigationService navigator;
        readonly RecoverWalletController controller;
        readonly IUserDialogs userDialogs;

        public RecoverViewModel(
            RecoverWalletController controller,
            INavigationService navigator,
            IUserDialogs userDialogs)
        {
            this.userDialogs = userDialogs;
            this.controller = controller;
            this.navigator = navigator;
        }

        ICommand _ResetCommand;
        public ICommand ResetCommand
        {
            get { return (_ResetCommand = _ResetCommand ?? new Command<string>(ExecuteResetCommand, CanExecuteResetCommand)); }
        }
        bool CanExecuteResetCommand(string obj) => true;
        async void ExecuteResetCommand(string passcode)
        {
            controller.SetPasscode(Passcode);

            var isValid = controller.VerifyPasscode(PasscodeConfirmation)
                                    && await controller.VerifySeedWords(Words.ToLower());

            if (false == isValid)
            {
                userDialogs.Toast("Invalid 12-word phrase or invalid passcode confirmation");

                return;
            }

            await navigator.NavigateAsync(NavigationKeys.RecoverWalletOk);
        }
    }
}
