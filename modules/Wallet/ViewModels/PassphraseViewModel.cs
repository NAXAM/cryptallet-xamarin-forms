using Wallet.Services;
using Wallet.Core;
namespace Wallet.ViewModels
{
    public class PassphraseViewModel : ViewModelBase
    {
        readonly NewWalletController controller;

        public PassphraseViewModel(NewWalletController controller)
        {
            this.controller = controller;
        }
    }
}
