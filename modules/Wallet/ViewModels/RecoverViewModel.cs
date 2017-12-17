using System.Windows.Input;
using Wallet.Core;
using Wallet.Services;
using Xamarin.Forms;

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

        public RecoverViewModel(
            RecoverWalletController controller,
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
            controller.SetPasscode(Passcode);

            var isValid = controller.VerifyPasscode(PasscodeConfirmation)
                                    && await controller.VerifySeedWords(Words);

            if (false == isValid) return;

            await navigator.NavigateAsync(NavigationKeys.RecoverWalletOk);
        }
    }
}
