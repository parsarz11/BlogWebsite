using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using WeblogApp.Data.Context;
using WeblogApp.Exceptions;
using WeblogApp.Model;
using WeblogApp.Model.DTOs.AccountingDTO;
using WeblogApp.Services.JwtServices;
using WeblogApp.Services.mailService;

namespace WeblogApp.Services.userAccounting
{
    public class Accounting : IUserAccounting
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly mailServices _mailServices;
        public Accounting(UserManager<IdentityUser> identityDbContext, SignInManager<IdentityUser> signInManager, JwtSettings jwtSettings, mailServices mail)
        {
            _userManager = identityDbContext;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings;
            _mailServices = mail;
        }


        public string Register(RegisterDTO identityUser)
        {
            var user = new IdentityUser()
            {
                UserName = identityUser.UserName,
                Email = identityUser.Email,
                //TwoFactorEnabled = true,
            };
            var result = _userManager.CreateAsync(user, identityUser.Password).Result;
            var role = _userManager.AddToRoleAsync(user,"User").Result;
            if (result.Errors.Count() > 0)
            {
                var ex = new Exception();

                foreach (var item in result.Errors)
                {
                    ex.Data.Add(item.Code, item);
                }

                throw ex;
            }
            //var mailToken = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
            //string callbackUrl = identityUser.domainName+"/confirmEmail.html" + "?userId=" + user.Id + "&" + "token=" + mailToken;
            //_mailServices.SendEmail(callbackUrl, user.Email);

            TokenGenModel tokenGenModel = new TokenGenModel()
            {
                UserName = user.UserName,
                Password = identityUser.Password,
            };


            return GenToken(tokenGenModel);
        }

        public string GenToken(TokenGenModel tokenGenDTO, bool isPersistent = false)
        {
            var user = _userManager.FindByNameAsync(tokenGenDTO.UserName).Result;
            //var token = _userManager.GetAuthenticationTokenAsync(user,"","test").Result;
            var result = _signInManager.PasswordSignInAsync(user, tokenGenDTO.Password, isPersistent, false).Result;

            //var addingRole = _userManager.AddToRoleAsync(user, "User").Result;
            //---------

            var token = new UserToken();

            var userRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
            if (userRole == null)
            {
                userRole = "User";
            }
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

                throw new NotFoundException(result.IsNotAllowed.ToString());
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

        public void confirmEmail(string userId, string token)
        {
            var user = _userManager.FindByIdAsync(userId).Result;

            if (user == null)
            {
                throw new NotFoundException("user is not found");
            }

            var result = _userManager.ConfirmEmailAsync(user, token).Result;

            if (result.Errors.Count() > 0)
            {
                var ex = new Exception();
                foreach (var item in result.Errors)
                {
                    ex.Data.Add(item.Code,item.Description);
                }
                throw ex;
            }
        }


        public void forgetPassword(string email)
        {
            
            var user = _userManager.FindByEmailAsync(email).Result;
            if (user == null || _userManager.IsEmailConfirmedAsync(user).Result == false)
            {
                throw new NotFoundException("user not found or user's email is not confirmed");
            }

            string forgetPasswordToken = _userManager.GeneratePasswordResetTokenAsync(user).Result;

            string url = "http://127.0.0.1:5500/changePassword.html" + "?" + "userId=" + user.Id + "&" + "token=" + forgetPasswordToken;

            _mailServices.SendEmail(url,user.Email);
        }

        public void ResetPassword(resetPasswordDTO resetPasswordDTO)
        {
            var user = _userManager.FindByIdAsync(resetPasswordDTO.userId).Result;
            if (user == null)
                throw new NotFoundException("userNotFound");
            var result = _userManager.ResetPasswordAsync(user,resetPasswordDTO.token,resetPasswordDTO.Password).Result;

            if (result.Errors.Count() > 0)
            {
                var ex = new Exception();
                foreach (var item in result.Errors)
                {
                    ex.Data.Add(item.Code, item.Description);
                }
                throw ex;
            }
        }

      
    }
}
