using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeblogApp.Model.DTOs.AccountingDTO;
using WeblogApp.Services.userAccounting;

namespace WeblogApp.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UsersAccountingController : Controller
    {
        private readonly IUserAccounting _userAccounting;

        public UsersAccountingController(IUserAccounting userAccounting)
        {
            _userAccounting = userAccounting;
        }

        [HttpPost]
        public IActionResult RegisterUser(RegisterDTO identityUser)
        {
            var result = _userAccounting.Register(identityUser);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO) 
        {
            return Ok(_userAccounting.logIn(loginDTO));
        }

        [HttpPost]
        public IActionResult confirmEmail(confirmEmailDTO confirmEmail)
        {
            _userAccounting.confirmEmail(confirmEmail.userId, confirmEmail.token);
            return Ok();
        }

        [HttpPost]
        public IActionResult forgetPassword(ForgetPasswordDTO forgetPasswordDTO)
        {
            _userAccounting.forgetPassword(forgetPasswordDTO.email);
            return Ok();
        }

        [HttpPost]
        public IActionResult resetPassword(resetPasswordDTO passwordDTO)
        {
            _userAccounting.ResetPassword(passwordDTO);
            return Ok();
        }
        [HttpGet]
        public IActionResult googleLogin()
        {
            //_userAccounting.ExternalLogin();
            return Ok(_userAccounting.ExternalLogin());
        }
    }
}
