using IdentityBugetoTest.Models;
using Microsoft.AspNetCore.Identity;
using WeblogApp.Model;
using WeblogApp.Model.DTOs;

namespace WeblogApp.Data.Repositories
{
    public interface IUserAccounting
    {
        public string Register(RegisterDTO registerDTO);
        public string logIn(LoginDTO loginDTO);
        public string GenToken(TokenGenModel tokenGenDTO, bool isPersistent = false);
    }
}
