using BitBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitBuy.UserService
{
    public interface IUserService
    {
        Task<string> Register(RegisterRequest model);
        Task<string> Authenticate(AuthenticateRequest model);
        ProfileModel GetUser(Guid id);
        Task<string> EditUserAsync(EditUserRequestModel model);
        Task<string> VerifyTwoFactorCode(TwoFactorRequestModel model);
    }
}
