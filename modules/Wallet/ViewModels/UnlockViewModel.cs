using Wallet.Services;
namespace Wallet.ViewModels
{
    using System.Windows.Input;
    using Prism.Navigation;
    using Wallet.Core;
    using Xamarin.Forms;

    public partial class UnlockViewModel : ViewModelBase
    {
        readonly IWalletManager walletManager;
        readonly INavigationService navigator;

        string _Passcode;
        public string Passcode
        {
            get => _Passcode;
            set => SetProperty(ref _Passcode, value);
        }

        public UnlockViewModel(
            INavigationService navigator,
            IWalletManager walletManager
        )
        {
            this.walletManager = walletManager;
            this.navigator = navigator;

            ShouldShowNavigationBar = false;
        }

        ICommand _UnlockCommand;
        public ICommand UnlockCommand
        {
            get { return (_UnlockCommand = _UnlockCommand ?? new Command<object>(ExecuteUnlockCommand, CanExecuteUnlockCommand)); }
        }
        bool CanExecuteUnlockCommand(object obj) => true;
        async void ExecuteUnlockCommand(object obj)
        {
            var unlocked = await walletManager.UnlockWalletAsync(Passcode);

            if (unlocked == false) return;

            await navigator.NavigateAsync(NavigationKeys.UnlockWallet);
        }
    }

    partial class UnlockViewModel
    {
        ICommand _CreateCommand;
        public ICommand CreateCommand
        {
            get { return (_CreateCommand = _CreateCommand ?? new Command<object>(ExecuteCreateCommand, CanExecuteCreateCommand)); }
        }
        bool CanExecuteCreateCommand(object obj) => true;
        async void ExecuteCreateCommand(object obj)
        {
            await navigator.NavigateAsync(NavigationKeys.CreateWallet);
        }

        ICommand _RecoverCommand;
        public ICommand RecoverCommand
        {
            get { return (_RecoverCommand = _RecoverCommand ?? new Command<object>(ExecuteRecoverCommand, CanExecuteRecoverCommand)); }
        }
        bool CanExecuteRecoverCommand(object obj) => true;
        async void ExecuteRecoverCommand(object obj)
        {
            await navigator.NavigateAsync(NavigationKeys.RecoverWallet);
        }
    }
}

