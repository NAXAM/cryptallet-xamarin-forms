using Wallet.Services;
using Wallet.Core;
using System.Windows.Input;
using Xamarin.Forms;
using Prism.Navigation;

namespace Wallet.ViewModels
{
    public class PassphraseViewModel : ViewModelBase
    {
        public string Words => string.Join(" ", controller.GetSeedWords());

        readonly NewWalletController controller;
        readonly INavigationService navigator;

        public PassphraseViewModel(
            NewWalletController controller,
            INavigationService navigator)
        {
            this.navigator = navigator;
            this.controller = controller;
        }

        ICommand _ContinueCommand;
        public ICommand ContinueCommand
        {
            get { return (_ContinueCommand = _ContinueCommand ?? new Command<string>(ExecuteContinueCommand, CanExecuteContinueCommand)); }
        }
        bool CanExecuteContinueCommand(string obj) => true;
        async void ExecuteContinueCommand(string obj)
        {
            await navigator.NavigateAsync(NavigationKeys.ConfirmPassphrase);
        }
    }
}
