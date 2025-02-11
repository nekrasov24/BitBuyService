using Microsoft.Extensions.Configuration;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace
    BitBuy.BlockchainService
{
    public class BlockchainService
    {
        private readonly Web3 _web3;
        private readonly Contract _nftContract;
        private readonly IConfiguration _configuration;


        public BlockchainService(string providerUrl, string contractAddress, string contractAbi, string sellerAddress, IConfiguration configuration)
        {
            _web3 = new Web3(providerUrl);
            _nftContract = _web3.Eth.GetContract(contractAbi, contractAddress);
            _configuration = configuration;
        }


        public async Task<string> MakeNFT(string ownerAddress, string tokenUri)
        {
            var contractAddress = "0xYourContractAddress";
            var abi = "MyContractABI";

            var web3 = new Web3(_configuration["ConnectionStrings:InfuraConnectionString"]);

            var contract = web3.Eth.GetContract(abi, contractAddress);

            var mintFunction = contract.GetFunction("mint");

            var transactionHash = await mintFunction.SendTransactionAsync(ownerAddress, ownerAddress, tokenUri);
            return transactionHash;
        }


        public async Task<bool> GetBalance(string buyerAddress, decimal price)
        {         
            var buyerBalance = await _web3.Eth.GetBalance.SendRequestAsync(buyerAddress);
            var requiredWei = Web3.Convert.ToWei(price);

            if (buyerBalance.Value < requiredWei)
                return false;
            return true;
        }

        public async Task<string> TransferEth(string buyerAddress, string sellerAddress, decimal price) //sent to buyer
        {
            var transferEthFunction = _nftContract.GetFunction("transferEth");
            var requiredWei = Web3.Convert.ToWei(price);
            var gas = await transferEthFunction.EstimateGasAsync(buyerAddress, sellerAddress, requiredWei);
            var transactionHash = await transferEthFunction.SendTransactionAsync(
                buyerAddress, gas, new HexBigInteger(0), new HexBigInteger(requiredWei), sellerAddress);
            return transactionHash;

        }

        public async Task<string> TransferNft(string buyerAddress, string sellerAddress, decimal price) //sent to seller
        {
            var transferNftFunction = _nftContract.GetFunction("transferNft");
            var requiredWei = Web3.Convert.ToWei(price);
            var gas = await transferNftFunction.EstimateGasAsync(buyerAddress, sellerAddress, requiredWei);
            var transactionHash = await transferNftFunction.SendTransactionAsync(
                buyerAddress, gas, new HexBigInteger(0), new HexBigInteger(requiredWei),sellerAddress);
            return transactionHash;
        }
    }
}
