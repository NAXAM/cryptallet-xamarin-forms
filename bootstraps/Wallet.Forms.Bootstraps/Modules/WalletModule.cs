
using Wallet.Views;
using Wallet.ViewModels;
using Prism.Modularity;
using Prism.Ioc;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Wallet.Services;

namespace Wallet.Forms.Bootstraps.Modules
{
    public class WalletModule : IModule
    {
        public void Initialize()
        {
        }

        public void OnInitialized()
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAccountsManager, AccountsManager>();
            containerRegistry.RegisterSingleton<NewWalletController>();
            containerRegistry.RegisterSingleton<IWalletManager, WalletManager>();

            containerRegistry.Register<INavigationService, WalletNavigationService>();

            containerRegistry.RegisterForNavigation<UnlockView, UnlockViewModel>();
            containerRegistry.RegisterForNavigation<PasscodeView, PasscodeViewModel>();
            containerRegistry.RegisterForNavigation<PasscodeConfirmationView, PasscodeConfirmationViewModel>();
            containerRegistry.RegisterForNavigation<PassphraseView, PassphraseViewModel>();
            containerRegistry.RegisterForNavigation<PassphraseConfirmationView, PasscodeConfirmationViewModel>();
            containerRegistry.RegisterForNavigation<WalletView, WalletViewModel>();
            containerRegistry.RegisterForNavigation<RecoverView, RecoverViewModel>();
        }

        class WalletNavigationService : INavigationService
        {
            readonly Prism.Navigation.INavigationService navigationService;

            public WalletNavigationService(Prism.Navigation.INavigationService navigationService)
            {
                this.navigationService = navigationService;
            }

            public async Task NavigateAsync(string key, IDictionary<string, string> parameters = null)
            {
                Uri uri = null;

                switch (key)
                {
                    case Wallet.NavigationKeys.UnlockWallet:
                        uri = Routes.Home;
                        break;
                    case Wallet.NavigationKeys.CreateWallet:
                        uri = Routes.WalletPasscode;
                        break;
                    case Wallet.NavigationKeys.ConfirmPasscode:
                        uri = Routes.WalletPasscodeConfirmation;
                        break;
                    case Wallet.NavigationKeys.ConnfirmPasscodeOk:
                        uri = Routes.WalletPassphrase;
                        break;
                    case Wallet.NavigationKeys.ConfirmaPassphrase:
                        uri = Routes.WalletPasscodeConfirmation;
                        break;
                    case Wallet.NavigationKeys.ConfirmaPassphraseOk:
                        uri = Routes.Wallet;
                        break;
                    case Wallet.NavigationKeys.RecoverWallet:
                        uri = Routes.WalletRecover;
                        break;

                    default:
                        await navigationService.NavigateAsync(key);
                        return;
                }

                await navigationService.NavigateAsync(uri);
            }
        }
    }
}

