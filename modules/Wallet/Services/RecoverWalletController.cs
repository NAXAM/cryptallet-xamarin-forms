using System;
using Nethereum.RPC.Eth.Services;
using Wallet.Controls;
using System.Threading.Tasks;
namespace Wallet.Services
{
    public class RecoverWalletController
    {
        readonly IWalletManager walletManager;

        string passcode;

        public RecoverWalletController(IWalletManager walletManager)
        {
            this.walletManager = walletManager;
        }

        public async Task<bool> VerifySeedWords(string seedWords)
        {
            if (string.IsNullOrWhiteSpace(seedWords)) return false;

            var words = seedWords.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var actual = string.Join(" ", words);

            return await walletManager.RestoreWallet(actual, passcode);
        }

        public void SetPasscode(string passcode)
        {
            this.passcode = passcode;
        }

        public bool VerifyPasscode(string passcodeConfirmation)
        {
            return string.Equals(passcode, passcodeConfirmation);
        }
    }
}
