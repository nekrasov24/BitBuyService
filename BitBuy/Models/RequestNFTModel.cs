﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitBuy.Models
{
    public class RequestNFTModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string OwnerAddress { get; set; }
        public bool IsListed { get; set; }
        public string TokenUri { get; set; }
    }
}
