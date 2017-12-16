using System;
using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.Eth.Transactions;
using Nethereum.RPC.TransactionManagers;
using Nethereum.StandardTokenEIP20;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.RPC.Accounts;

namespace Wallet.Core
{
    class WorkaroundStandardTokenService : StandardTokenService
    {
        public WorkaroundStandardTokenService(Web3 web3, string address) : base(web3, address)
        {

        }

        public new async Task<string> TransferAsync<T>(string addressFrom, string addressTo, T value, HexBigInteger gas = null)
        {
            if (gas == null)
            {
                var function = GetTransferFunction();

                gas = await function.EstimateGasAsync(addressFrom, null, null, addressTo, value);
            }

            return await base.TransferAsync(addressFrom, addressTo, value, gas);
        }
    }
}
