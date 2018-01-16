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
using System.Threading.Tasks;
using Nethereum.JsonRpc.Client;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.StandardTokenEIP20.Events.DTO;
using Wallet.Core;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexConvertors.Extensions;
using Wallet.Models;
using System.Linq;
using Xamarin.Forms.Internals;
using Nethereum.Contracts;

namespace Wallet.Services
{
    public interface IAccountsManager
    {
        string DefaultAccountAddress { get; }

        Task<string[]> GetAccountsAsync();

        Task<decimal> GetTokensAsync(string accountAddress);
        Task<decimal> GetBalanceInETHAsync(string accountAddress);

        Task<TransactionModel[]> GetTransactionsAsync(bool sent = false);

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

        public Task<TransactionModel[]> GetTransactionsAsync(bool sent = false)
        {
            return Task.Run(async delegate
            {
                var transferEvent = standardTokenService.GetTransferEvent();

                var paddedAccountAddress = DefaultAccountAddress.RemoveHexPrefix()
                                                 .PadLeft(64, '0')
                                                 .EnsureHexPrefix();

                var filter = transferEvent.CreateFilterInput(
                    new object[] { sent ? paddedAccountAddress : null },
                    new object[] { sent ? null : paddedAccountAddress },
                    BlockParameter.CreateEarliest(),
                    BlockParameter.CreateLatest());

                var changes = await transferEvent.GetAllChanges<Transfer>(filter);

                var timestampTasks = changes.Select(x => Task.Factory.StartNew(async (state) =>
                {
                    var log = (EventLog<Transfer>)state;

                    var block = await web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(log.Log.BlockNumber);

                    return new TransactionModel
                    {
                        Sender = log.Event.AddressFrom,
                        Receiver = log.Event.AddressTo,
                        Amount = (decimal)log.Event.Value,
                        Inward = false == sent,
                        Timestamp = (long)block.Timestamp.Value
                    };
                }, x));

                return await Task.WhenAll(timestampTasks).ContinueWith(tt => {
                    return tt.Result.Select(x => x.Result).ToArray();
                });
            });
        }

        void Initialize()
        {
            //var client = new RpcClient(new Uri("http://192.168.12.154:8545"));//iOS
            //var client = new RpcClient(new Uri("http://10.0.2.2:8545"));//ANDROID
            var client = new RpcClient(new Uri("https://rinkeby.infura.io/O3CUsRfVYECJi12W8fk3"));

            web3 = new Web3(DefaultAccount, client);
            standardTokenService = new WorkaroundStandardTokenService(web3, CONTRACT_ADDRESS);
        }
    }

}
