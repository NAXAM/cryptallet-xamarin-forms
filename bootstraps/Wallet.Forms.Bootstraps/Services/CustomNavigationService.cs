/*
 * Copyright 2018 NAXAM CO.,LTD.
 *
 *   Licensed under the Apache License, Version 2.0 (the "License");
 *   you may not use this file except in compliance with the License.
 *   You may obtain a copy of the License at
 *
 *       http://www.apache.org/licenses/LICENSE-2.0
 *
 *   Unless required by applicable law or agreed to in writing, software
 *   distributed under the License is distributed on an "AS IS" BASIS,
 *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *   See the License for the specific language governing permissions and
 *   limitations under the License.
 */ 
using System;
using Prism.Navigation;
using Prism.Behaviors;
using Prism.Common;
using Prism.Ioc;
using Prism.Logging;

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
                case Wallet.NavigationKeys.WalletViewHistory:
                    uri = Routes.WalletViewHistory;
                    break;

                default:
                    await NavigateAsync(name, parameters);
                    return;
            }

            await NavigateAsync(uri, parameters);
        }
    }
}

