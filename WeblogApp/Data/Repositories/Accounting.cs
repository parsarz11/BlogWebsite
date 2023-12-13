using IdentityBugetoTest.Models;
using IdentityBugetoTest.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using WeblogApp.Data.Context;
using WeblogApp.Model;

namespace WeblogApp.Data.Repositories
{
    public class Accounting:IUserAccounting
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public Accounting(UserManager<IdentityUser> identityDbContext, SignInManager<IdentityUser> signInManager, JwtSettings jwtSettings)
        {
            _userManager = identityDbContext;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings;
        }

        public string Register(RegisterDTO identityUser)
        {
            var user = new IdentityUser()
            {
                UserName = identityUser.UserName,
                Email = identityUser.Email,

            };
            var result = _userManager.CreateAsync(user,identityUser.Password).Result;
            return result.ToString();
        }

        public UserToken LogIn(LoginDTO loginDTO)
        {
            var user = _userManager.FindByNameAsync(loginDTO.UserName).Result;
            //var token = _userManager.GetAuthenticationTokenAsync(user,"","test").Result;
            var result = _signInManager.PasswordSignInAsync(user, loginDTO.Password, loginDTO.IsPersistent, false).Result;

            //var addingRole = _userManager.AddToRoleAsync(user, "User").Result;
            //---------

            var token = new UserToken();

            var userRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault();

            if (result.Succeeded)
            {
                //var user = user.FirstOrDefault(x => x.UserName.Equals(loginDTO.UserName, StringComparison.OrdinalIgnoreCase));
                token = JwtHelper.GenTokenkey(new UserToken()
                {
                    EmailId = user.Email,
                    GuidId = Guid.NewGuid(),
                    UserName = user.UserName,
                    Id = user.Id,
                    Role = userRole,
                }, _jwtSettings);


            }
            else
            {
                throw new Exception();
            }

            return token;


            //---------

            //var tokenClient = new TokenClient();
            //if (result.Succeeded)
            //{
            //    return "";
            //}

            //return "";

        }
    }
}
