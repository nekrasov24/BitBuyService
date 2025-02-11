using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BitBuy.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(100)]
        public DateTime? DateOfBirth { get; set; }
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(15)]
        public string Phone { get; set; }
        [StringLength(100)]
        public string Password { get; set; }
        public Roles Roles { get; set; }
        public TwoFactorAuthenticationSwitch TwoFactorAuthenticationSwitch { get; set; }
        public string TwoFactorCode { get; set; }
        public decimal AccountBalance { get; set; }
        public string WalletAddress { get; set; }
    }
}
