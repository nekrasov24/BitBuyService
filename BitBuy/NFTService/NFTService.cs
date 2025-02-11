using BitBuy.BlockchainService;
using BitBuy.Models;
using BitBuy.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BitBuy.NFTService
{
    public class NFTService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IRepository<User, Guid> _userRepository;
        private readonly IBlockchainService _blockchainService;
        private readonly IRepository<NFT, Guid> _nftRepository;
        private readonly IRepository<Transaction, Guid> _transactionRepository;

        public NFTService(IHostingEnvironment hostingEnvironment, IRepository<User, Guid> userRepository, IBlockchainService blockchainService,
            IRepository<NFT, Guid> nftRepository, IRepository<Transaction, Guid> transactionRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _userRepository = userRepository;
            _blockchainService = blockchainService;
            _nftRepository = nftRepository;
            _transactionRepository = transactionRepository;
        }
        

        public async Task<string> CreateNFT(RequestNFTModel model)
        {
            var transactionHash = await _blockchainService.MakeNFT(model.OwnerAddress, model.TokenUri);

            var newCreatedNFT = new NFT
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = imageUrl,
                OwnerAddress = model.OwnerAddress,
                Price = model.Price,
                IsListed = false,
            };

            await _nftRepository.AddAsync(newCreatedNFT);
            return transactionHash;
        }

        public async Task ListNft(Guid nftId, decimal price)
        {
            var nft = _nftRepository.GetNFTById(nftId);            
            nft.Price = price;
            nft.IsListed = true;
            await _nftRepository.SaveChangesAsync();
        }

        public async Task<IQueryable<NFT>> GetListedNfts()
        {
            var nfts = await _nftRepository.GetAllAsync(w => w.IsListed);
            return nfts;
        }

        public async Task BuyNft(Guid nftId, string buyerAddress)
        {
            try
            {
                var nft = _nftRepository.GetNFTById(nftId);
                if (nft == null || !nft.IsListed)
                    throw new Exception("NFT is not available for sale.");
                var sellerAddress = nft.OwnerAddress;

                var buyer = _userRepository.GetUserByWalletAddress(buyerAddress);
                if (buyer == null)
                    throw new Exception("Buyer was not found.");

                var buyerBalance = await _blockchainService.GetBalance(buyerAddress, nft.Price);
                if (buyerBalance is false)
                    throw new Exception("you don't not have enough ETH.");

                var transactionHash = await _blockchainService.TransferNft(buyerAddress, sellerAddress, nft.Price);

                nft.OwnerAddress = buyerAddress;
                nft.IsListed = false;
                await _nftRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task RecordTransaction(Guid nftId, string fromAddress, string toAddress, decimal price)
        {
            var transaction = new Transaction
            {
                NftId = nftId,
                FromAddress = fromAddress,
                ToAddress = toAddress,
                Price = price,
                TransactionDate = DateTime.UtcNow
            };

            await _transactionRepository.AddAsync(transaction);
        }
    }
}
