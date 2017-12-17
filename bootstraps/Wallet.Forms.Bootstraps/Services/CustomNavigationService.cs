using System;

using Xamarin.Forms;
using Prism.Navigation;
using Prism.Behaviors;
using Prism.Common;
using Prism.Ioc;
using Prism.Logging;
using Wallet.Core;
using System.Threading.Tasks;

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

        protected async override Task<bool> GoBackInternal(NavigationParameters parameters, bool? useModalNavigation, bool animated)
        {
            try
            {
                NavigationSource = PageNavigationSource.NavigationService;

                var page = GetCurrentPage();
                var segmentParameters = UriParsingHelper.GetSegmentParameters(null, parameters);
                segmentParameters.AddInternalParameter("__NavigationMode", NavigationMode.Back);

                var canNavigate = await PageUtilities.CanNavigateAsync(page, segmentParameters);
                if (!canNavigate)
                    return false;

                bool useModalForDoPop = UseModalNavigation(page, useModalNavigation);
                Page previousPage = PageUtilities.GetOnNavigatedToTarget(page, _applicationProvider.MainPage, useModalForDoPop);

                PageUtilities.OnNavigatingTo(previousPage, segmentParameters);

                var poppedPage = await DoPop(page.Navigation, useModalForDoPop, animated);
                if (poppedPage != null)
                {
                    PageUtilities.OnNavigatedFrom(page, segmentParameters);
                    PageUtilities.OnNavigatedTo(previousPage, segmentParameters);
                    PageUtilities.DestroyPage(poppedPage);
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.Log(e.ToString(), Category.Exception, Priority.High);
                return false;
            }
            finally
            {
                NavigationSource = PageNavigationSource.Device;
            }

            return false;
        }

        internal static bool UseModalNavigation(Page currentPage, bool? useModalNavigationDefault)
        {
            return false;
        }
    }
}

