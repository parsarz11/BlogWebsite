using IdentityBugetoTest.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeblogApp.Data.Repositories;
using WeblogApp.Model;

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
            return View(result);
        }

        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO) 
        {
            return Ok(_userAccounting.LogIn(loginDTO));
        }
    }
}
