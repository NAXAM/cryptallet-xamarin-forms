using Prism.Autofac;
using Prism;
using Wallet.Views;
using Wallet.Forms.Bootstraps.Modules;
using Xamarin.Forms;
using System;

namespace Wallet.Forms.Bootstraps
{
    public partial class WalletApplication : PrismApplication
    {
        public WalletApplication(IPlatformInitializer platformInitializer = null) : base(platformInitializer)
        {
            //MainPage = new ContentPage();
        }

        protected async override void OnInitialized()
        {
            Initialize();

            await NavigationService.NavigateAsync(Routes.Home);
        }

        protected override void RegisterTypes(Prism.Ioc.IContainerRegistry containerRegistry)
        {
            new WalletModule().RegisterTypes(containerRegistry);
        }
    }

    public static class Routes
    {
        static readonly string appScheme = "cryptallet";

        public static readonly Uri Home = new Uri($"{appScheme}:///{nameof(UnlockView)}");
    }

}
