using Prism.DryIoc;
using Prism;
using Wallet.Views;
using Wallet.Forms.Bootstraps.Modules;
using Xamarin.Forms;
using System;
using Plugin.SecureStorage;
using Prism.Ioc;

namespace Wallet.Forms.Bootstraps
{
    public partial class WalletApplication : PrismApplication
    {
        public WalletApplication(IPlatformInitializer platformInitializer = null) : base(platformInitializer)
        {
        }

        protected async override void OnInitialized()
        {
            Initialize();

            await NavigationService.NavigateAsync(Routes.Home);
        }

        protected override void RegisterTypes(Prism.Ioc.IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(CrossSecureStorage.Current);
        }

        protected override void ConfigureModuleCatalog(Prism.Modularity.IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

            moduleCatalog.AddModule(new Prism.Modularity.ModuleInfo(typeof(WalletModule)));
        }
    }

    public static class Routes
    {
        static readonly string appScheme = "cryptallet";

        public static readonly Uri Home = new Uri($"{appScheme}:///{nameof(UnlockView)}");
    }

}
