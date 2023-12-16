using IdentityBugetoTest.Models;
using Microsoft.AspNetCore.Identity;
using WeblogApp.Data.Context;
using WeblogApp.Exceptions;
using WeblogApp.Model;
using WeblogApp.Model.DTOs;

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
            if (result.Errors.Count() > 0)
            {
                var ex = new Exception();

                foreach (var item in result.Errors)
                {
                    ex.Data.Add(item.Code, item);
                }

                throw ex;
            }
            

            TokenGenModel tokenGenModel = new TokenGenModel()
            {
                UserName = user.UserName,
                Password = identityUser.Password,
            };
            ;
            
            return GenToken(tokenGenModel);
        }

        public string GenToken(TokenGenModel tokenGenDTO,bool isPersistent = false)
        {
            var user = _userManager.FindByNameAsync(tokenGenDTO.UserName).Result;
            //var token = _userManager.GetAuthenticationTokenAsync(user,"","test").Result;
            var result = _signInManager.PasswordSignInAsync(user, tokenGenDTO.Password, isPersistent, false).Result;

            //var addingRole = _userManager.AddToRoleAsync(user, "User").Result;
            //---------

            var token = new UserToken();

            var userRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault();

            if (result.Succeeded)
            {
                
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
                
                throw new NotFoundException(result.ToString());
            }

            return token.Token;




        }

        public string logIn(LoginDTO loginDTO)
        {
            var token = new TokenGenModel()
            {
                UserName = loginDTO.UserName,
                Password = loginDTO.Password,
            };

            return GenToken(token, loginDTO.IsPersistent);
        }
    }
}
