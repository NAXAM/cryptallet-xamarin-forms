using Prism.DryIoc;
using Prism;
using Wallet.Views;
using Wallet.Forms.Bootstraps.Modules;
using Xamarin.Forms;
using System;
using Plugin.SecureStorage;
using Prism.Ioc;
using Prism.Navigation;
using Wallet.Forms.Bootstraps.Services;
using DryIoc;

namespace Wallet.Forms.Bootstraps
{
    public partial class WalletApplication : PrismApplication
    {
        public WalletApplication(IPlatformInitializer platformInitializer = null) : base(platformInitializer)
        {
        }

        protected async override void OnInitialized()
        {
            InitializeComponent();

            //NavigationService = Container.Resolve<INavigationService>();

            await NavigationService.NavigateAsync(Routes.Default);
        }

        protected override void RegisterTypes(Prism.Ioc.IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(CrossSecureStorage.Current);

            Container.GetContainer().Register<INavigationService, CustomNavigationService>(
                serviceKey: NavigationServiceName,
                ifAlreadyRegistered: IfAlreadyRegistered.Replace
            );

            containerRegistry.RegisterForNavigation<NavigationPage>();
        }

        protected override void ConfigureModuleCatalog(Prism.Modularity.IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

            moduleCatalog.AddModule(new Prism.Modularity.ModuleInfo(typeof(WalletModule)));
        }
    }

    public static partial class Routes
    {
        static readonly string appScheme = "cryptallet";
        static readonly string navigation = nameof(NavigationPage);

        public static readonly Uri Home = new Uri($"{appScheme}:///{navigation}/{nameof(WalletView)}", UriKind.Absolute);
        public static readonly Uri Default = new Uri($"{appScheme}:///{navigation}/{nameof(UnlockView)}", UriKind.Absolute);
    }

    partial class Routes
    {
        public static readonly Uri WalletPasscode = new Uri($"{nameof(PasscodeView)}", UriKind.Relative);
        public static readonly Uri WalletPasscodeConfirmation = new Uri($"{nameof(PasscodeConfirmationView)}", UriKind.Relative);
        public static readonly Uri WalletPassphrase = new Uri($"{nameof(PassphraseView)}", UriKind.Relative);
        public static readonly Uri WalletPassphraseConfirmation = new Uri($"{nameof(PassphraseConfirmationView)}", UriKind.Relative);
        public static readonly Uri Wallet = new Uri($"{nameof(WalletView)}", UriKind.Relative);
        public static readonly Uri WalletRecover = new Uri($"{nameof(RecoverView)}", UriKind.Relative);
    }
}
