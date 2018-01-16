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
            var result = await navigationService.GoBackAsync(new NavigationParameters {
                {"qr_code", obj.Text}
            });
        }
    }
}
