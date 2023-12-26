using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

       
    }
}
