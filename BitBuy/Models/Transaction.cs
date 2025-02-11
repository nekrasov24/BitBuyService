using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitBuy.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid NftId { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public decimal Price { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
