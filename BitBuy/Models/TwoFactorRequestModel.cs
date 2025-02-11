using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitBuy.Models
{
    public class TwoFactorRequestModel
    {
        public string Email { get; set; }

        public string Code { get; set; }
    }
}
