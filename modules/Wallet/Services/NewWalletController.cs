using System.Threading.Tasks;
namespace Wallet.Services
{
    public class NewWalletController
    {
        readonly IWalletManager walletManager;

        string passcode;

        public NewWalletController(IWalletManager walletManager)
        {
            this.walletManager = walletManager;
        }

        public async Task CreateWalletAsync()
        {
            await walletManager.CreateWalletAsync();
        }

        public async Task SaveWalletAsync()
        {
            await walletManager.SaveWalletAsync(passcode);
        }

        public string[] GetSeedWords()
        {
            return walletManager.Wallet.Words;
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
