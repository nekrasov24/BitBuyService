using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitBuy.UserService
{
    public class TwoFactoryAuthentication
    {
        public byte[] GenerateTwoFactorSecret()
        {
            var secret = KeyGeneration.GenerateRandomKey(mode: OtpHashMode.Sha256);
            return secret;
        }

        public string GenerateTwoFactorCode(byte[] secret)
        {
            var newcode = new Totp(secret, 30);
            return newcode.ComputeTotp(DateTime.UtcNow);
        }

        public bool VerifyTotp(string code, byte[] secret)
        {
            long timeStep = 30;
            var topt = new Totp(secretKey: secret);
            return topt.VerifyTotp(code, out timeStep);
        }
    }
}
