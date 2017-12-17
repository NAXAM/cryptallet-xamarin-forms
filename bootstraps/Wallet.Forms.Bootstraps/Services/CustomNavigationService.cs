using System;

using Xamarin.Forms;
using Prism.Navigation;
using Prism.Behaviors;
using Prism.Common;
using Prism.Ioc;
using Prism.Logging;
using Wallet.Core;

namespace Wallet.Forms.Bootstraps.Services
{
    public class CustomNavigationService : PageNavigationService
    {
        public CustomNavigationService(IContainerExtension container, IApplicationProvider applicationProvider, IPageBehaviorFactory pageBehaviorFactory, ILoggerFacade logger) : base(container, applicationProvider, pageBehaviorFactory, logger)
        {
        }

        public async override System.Threading.Tasks.Task NavigateAsync(string name, NavigationParameters parameters)
        {
            Uri uri = null;

            switch (name)
            {
                case Wallet.NavigationKeys.UnlockWallet:
                case Wallet.NavigationKeys.RecoverWalletOk:
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
                case Wallet.NavigationKeys.ConfirmPassphrase:
                    uri = Routes.WalletPassphraseConfirmation;
                    break;
                case Wallet.NavigationKeys.ConfirmPassphraseOk:
                    uri = Routes.Wallet;
                    break;
                case Wallet.NavigationKeys.RecoverWallet:
                    uri = Routes.WalletRecover;
                    break;
                case Wallet.NavigationKeys.ScanQRCode:
                    uri = Routes.QRCodeScanner;
                    break;

                default:
                    await NavigateAsync(name, parameters);
                    return;
            }

            await NavigateAsync(uri, parameters);
        }
    }
}

