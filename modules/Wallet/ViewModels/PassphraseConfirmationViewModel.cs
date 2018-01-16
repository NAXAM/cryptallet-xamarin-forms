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
using System;
using System.Windows.Input;
using Prism.Navigation;
using Wallet.Core;
using Wallet.Services;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace Wallet.ViewModels
{
    public class PassphraseConfirmationViewModel : ViewModelBase
    {
        string _SecondWord;
        public string SecondWord
        {
            get => _SecondWord;
            set => SetProperty(ref _SecondWord, value);
        }

        string _FifthWord;
        public string FifthWord
        {
            get => _FifthWord;
            set => SetProperty(ref _FifthWord, value);
        }

        string _NinethWord;
        public string NinethWord
        {
            get => _NinethWord;
            set => SetProperty(ref _NinethWord, value);
        }

        readonly NewWalletController controller;
        readonly INavigationService navigator;
        readonly IUserDialogs userDialogs;

        public PassphraseConfirmationViewModel(
            NewWalletController controller,
            INavigationService navigator,
            IUserDialogs userDialogs
        )
        {
            this.userDialogs = userDialogs;
            this.controller = controller;
            this.navigator = navigator;
        }

        ICommand _ContinueCommand;
        public ICommand ContinueCommand
        {
            get { return (_ContinueCommand = _ContinueCommand ?? new Command<string>(ExecuteContinueCommand, CanExecuteContinueCommand)); }
        }
        bool CanExecuteContinueCommand(string obj) => true;
        async void ExecuteContinueCommand(string obj)
        {
            var words = controller.GetSeedWords();
            var isValid = string.Equals(SecondWord, words[1], StringComparison.InvariantCultureIgnoreCase)
                                && string.Equals(FifthWord, words[4], StringComparison.InvariantCultureIgnoreCase)
                                && string.Equals(NinethWord, words[8], StringComparison.InvariantCultureIgnoreCase);

            if (false == isValid)
            {
                userDialogs.Toast("Invalid words confirmation.");
                return;
            }

            await controller.SaveWalletAsync();
            await navigator.NavigateAsync(NavigationKeys.ConfirmPassphraseOk);
        }
    }
}
