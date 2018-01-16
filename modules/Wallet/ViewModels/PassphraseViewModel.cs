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
using Wallet.Services;
using Wallet.Core;
using System.Windows.Input;
using Xamarin.Forms;
using Prism.Navigation;

namespace Wallet.ViewModels
{
    public class PassphraseViewModel : ViewModelBase
    {
        public string Words => string.Join(" ", controller.GetSeedWords());

        readonly NewWalletController controller;
        readonly INavigationService navigator;

        public PassphraseViewModel(
            NewWalletController controller,
            INavigationService navigator)
        {
            this.navigator = navigator;
            this.controller = controller;
        }

        ICommand _ContinueCommand;
        public ICommand ContinueCommand
        {
            get { return (_ContinueCommand = _ContinueCommand ?? new Command<string>(ExecuteContinueCommand, CanExecuteContinueCommand)); }
        }
        bool CanExecuteContinueCommand(string obj) => true;
        async void ExecuteContinueCommand(string obj)
        {
            await navigator.NavigateAsync(NavigationKeys.ConfirmPassphrase);
        }
    }
}
