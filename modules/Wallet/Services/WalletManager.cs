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
namespace Wallet.Services
{
    using System.Threading.Tasks;
    using NBitcoin;
    using Nethereum.HdWallet;
    using Plugin.SecureStorage.Abstractions;
    using Nethereum.Hex.HexConvertors.Extensions;

    public interface IWalletManager
    {
        Wallet Wallet { get; }

        Task CreateWalletAsync();

        Task SaveWalletAsync(string password);

        Task<bool> UnlockWalletAsync(string password, bool bypass = false);

        Task<bool> RestoreWallet(string seedWords, string password);
    }

    public class WalletManager : IWalletManager
    {
        const string PASSWORD_KEY = "__wallet__password__";
        const string SEED_KEY = "__wallet__seed__";
        const string SEED_PASSWORD = "GNzheuuNsOkkBHG3hFSpA37UAQ1TDWQH0ncZcR2+7r4=";

        Wallet wallet;
        public Wallet Wallet => wallet;

        readonly ISecureStorage secureStorgage;

        public WalletManager(ISecureStorage secureStorgage)
        {
            this.secureStorgage = secureStorgage;
        }

        public Task CreateWalletAsync()
        {
            return Task.Run(delegate
            {
                wallet = new Wallet(Wordlist.English, WordCount.Twelve, SEED_PASSWORD);
            });
        }

        public Task SaveWalletAsync(string password)
        {
            return Task.Run(delegate
            {
                if (wallet == null) return;

                StoreCredentials(password);
            });
        }

        public Task<bool> UnlockWalletAsync(string password, bool bypass = false)
        {
            return Task.Run(delegate
            {
                if (false == bypass)
                {
                    var storedPassword = secureStorgage.GetValue(PASSWORD_KEY);

                    if (false == string.Equals(password, storedPassword)) return false;
                }

                var storedSeed = secureStorgage.GetValue(SEED_KEY);

                if (string.IsNullOrWhiteSpace(storedSeed)) return false;

                wallet = new Wallet(storedSeed.HexToByteArray());

                return true;
            });
        }

        public Task<bool> RestoreWallet(string seedWords, string password)
        {
            return Task.Run(delegate
            {
                wallet = new Wallet(seedWords, SEED_PASSWORD);

                var storedSeed = secureStorgage.GetValue(wallet.GetAccount(0).Address);

                return false == string.IsNullOrWhiteSpace(storedSeed);
            });
        }


        void StoreCredentials(string password)
        {
            secureStorgage.SetValue(PASSWORD_KEY, password);

            var account = wallet.GetAccount(0);

            secureStorgage.SetValue(SEED_KEY, wallet.Seed);
            secureStorgage.SetValue(account.Address, account.PrivateKey);
        }
    }
}
