using System;
using System.Windows.Input;
using Wallet.Core;
using Wallet.Services;
using Xamarin.Forms;

namespace Wallet.ViewModels
{
    public class PassphraseConfirmationViewModel : ViewModelBase
    {
        readonly NewWalletController controller;
        readonly INavigationService navigator;

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

        public PassphraseConfirmationViewModel(
            NewWalletController controller,
            INavigationService navigator)
        {
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
            var isValid = string.Equals(SecondWord, words[1])
                            && string.Equals(FifthWord, words[4])
                            && string.Equals(NinethWord, words[8]);

            if (false == isValid)
            {
                return;
            }

            await navigator.NavigateAsync(NavigationKeys.ConfirmPassphraseOk);
        }
    }
}
