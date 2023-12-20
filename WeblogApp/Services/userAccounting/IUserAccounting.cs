using Microsoft.AspNetCore.Identity;
using WeblogApp.Model;
using WeblogApp.Model.DTOs.AccountingDTO;

namespace WeblogApp.Services.userAccounting
{
    public interface IUserAccounting
    {
        public string Register(RegisterDTO registerDTO);
        public string logIn(LoginDTO loginDTO);

        public string GenToken(TokenGenModel tokenGenDTO, bool isPersistent = false);

        public void confirmEmail(string userId, string token);

        public void forgetPassword(string email);
        public void ResetPassword(resetPasswordDTO resetPasswordDTO);

        public void GenTwoFactorCode(string userName);
        public void TwoFactorauthconfirm(twoFactorAuthDTO authDTO);
    }
}
