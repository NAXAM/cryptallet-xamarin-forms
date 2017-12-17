using System;
using System.Threading.Tasks;
using Nethereum.JsonRpc.Client;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.StandardTokenEIP20.Events.DTO;
using Wallet.Core;

namespace Wallet.Services
{
    public interface IAccountsManager
    {
        string DefaultAccountAddress { get; }

        Task<string[]> GetAccountsAsync();

        Task<decimal> GetTokensAsync(string accountAddress);
        Task<decimal> GetBalanceInETHAsync(string accountAddress);

        Task GetTransactionsAsync(bool sent = false);

        Task<string> TransferAsync(string from, string to, decimal amount);
    }

    public class AccountsManager : IAccountsManager
    {
        const string CONTRACT_ADDRESS = "0xc66f5f66c672b645c3afb3387f4a81bb28f03984";

        public string DefaultAccountAddress => DefaultAccount?.Address;

        Account DefaultAccount => walletManager.Wallet?.GetAccount(0);

        readonly IWalletManager walletManager;
        WorkaroundStandardTokenService standardTokenService;
        Web3 web3;

        public AccountsManager(IWalletManager walletManager)
        {
            this.walletManager = walletManager;

            Initialize();
        }

        public async Task<string[]> GetAccountsAsync()
        {
            return new string[] { DefaultAccountAddress };
        }

        public async Task<decimal> GetTokensAsync(string accountAddress)
        {
            return await standardTokenService.GetBalanceOfAsync<int>(accountAddress);
        }

        public async Task<decimal> GetBalanceInETHAsync(string accountAddress)
        {
            var balanceInWei = await web3.Eth.GetBalance.SendRequestAsync(accountAddress);

            return Web3.Convert.FromWei(balanceInWei);
        }

        public async Task<string> TransferAsync(string from, string to, decimal amount)
        {
            return await standardTokenService.TransferAsync(from, to, amount);
        }

        public Task GetTransactionsAsync(bool sent = false)
        {
            return Task.Run(async delegate
            {
                var transferEvent = standardTokenService.GetTransferEvent();

                var filter = false == sent
                    ? await transferEvent.CreateFilterAsync(DefaultAccountAddress)
                    : await transferEvent.CreateFilterAsync<string, object, string>(null, null, DefaultAccountAddress);

                var changes = transferEvent.GetFilterChanges<Transfer>(filter);

                return changes;
            });
        }

        void Initialize()
        {
            //var client = new RpcClient(new Uri("http://127.0.0.1:9545"));//iOS
            //var client = new RpcClient(new Uri("http://10.0.2.2:9545"));//ANDROID
            var client = new RpcClient(new Uri("https://rinkeby.infura.io/O3CUsRfVYECJi12W8fk3"));

            web3 = new Web3(DefaultAccount, client);
            standardTokenService = new WorkaroundStandardTokenService(web3, CONTRACT_ADDRESS);
        }
    }

}
