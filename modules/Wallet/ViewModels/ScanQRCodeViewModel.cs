using Wallet.Core;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing;

namespace Wallet.ViewModels
{
    public class ScanQRCodeViewModel : ViewModelBase
    {
        readonly INavigationService navigationService;

        public ScanQRCodeViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        ICommand _ScanCommand;
        public ICommand ScanCommand
        {
            get { return (_ScanCommand = _ScanCommand ?? new Command<Result>(ExecuteScanCommand, CanExecuteScanCommand)); }
        }
        bool CanExecuteScanCommand(Result obj) => true;
        async void ExecuteScanCommand(Result obj)
        {
            await navigationService.GoBackAsync(new NavigationParameters {
                {"qr_code", obj.Text}
            });
        }
    }
}
