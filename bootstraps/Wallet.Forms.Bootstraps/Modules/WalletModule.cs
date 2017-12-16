using Prism.Modularity;
using Prism.Ioc;
using Wallet.Views;
using Wallet.ViewModels;

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
            containerRegistry.RegisterForNavigation<UnlockView, UnlockViewModel>();
        }
    }
}

