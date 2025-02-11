using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitBuy.Models
{
    public class AppContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<NFT> NFTs { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {

        }
    }
}
