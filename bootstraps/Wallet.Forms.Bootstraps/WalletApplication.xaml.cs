using Prism.DryIoc;
using Prism;
using Wallet.Views;
using Xamarin.Forms;
using System;
using Plugin.SecureStorage;
using Prism.Ioc;
using Prism.Navigation;
using Wallet.Forms.Bootstraps.Services;
using DryIoc;
using Plugin.Share;
using Prism.Common;
using Acr.UserDialogs;

namespace Wallet.Forms.Bootstraps
{
    public partial class WalletApplication : PrismApplicationBase
    {
        public WalletApplication(IPlatformInitializer platformInitializer = null) : base(platformInitializer)
        {
        }

        /// <summary>
        /// Creates the <see cref="IContainerExtension"/> for DryIoc
        /// </summary>
        /// <returns></returns>
        protected override IContainerExtension CreateContainerExtension()
        {
            return new DryIocContainerExtension(new Container(CreateContainerRules()));
        }

        /// <summary>
        /// Create <see cref="Rules" /> to alter behavior of <see cref="IContainer" />
        /// </summary>
        /// <returns>An instance of <see cref="Rules" /></returns>
        protected virtual Rules CreateContainerRules() => Rules.Default.WithAutoConcreteTypeResolution();

        /// <summary>
        /// Configures the Container.
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterRequiredTypes(containerRegistry);
            Container.GetContainer().Register<INavigationService, CustomNavigationService>();
            Container.GetContainer().Register<INavigationService>(
                made: Made.Of(() => SetPage(Arg.Of<INavigationService>(), Arg.Of<Page>())),
                setup: Setup.Decorator);
        }

        internal static INavigationService SetPage(INavigationService navigationService, Page page)
        {
            if (navigationService is IPageAware pageAware)
            {
                pageAware.Page = page;
            }

            return navigationService;
        }

        protected async override void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync(Routes.Default);
        }

        protected override void RegisterTypes(Prism.Ioc.IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(CrossSecureStorage.Current);
            containerRegistry.RegisterInstance(CrossShare.Current);
            containerRegistry.RegisterInstance(UserDialogs.Instance);

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
        public static readonly Uri QRCodeScanner = new Uri($"{nameof(ScanQRCodeView)}", UriKind.Relative);
    }
}
