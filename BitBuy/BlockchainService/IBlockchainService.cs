using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitBuy.BlockchainService
{
    public interface IBlockchainService
    {
        Task<string> MakeNFT(string ownerAddress, string tokenUri);
        Task<bool> GetBalance(string buyerAddress, decimal price);
        Task<string> TransferEth(string buyerAddress, string sellerAddress, decimal price);
        Task<string> TransferNft(string buyerAddress, string sellerAddress, decimal price);
    }
}
