using IdentityBugetoTest.Models.DTOs;
using IdentityBugetoTest.Models;
using Microsoft.AspNetCore.Identity;
using WeblogApp.Model;

namespace WeblogApp.Data.Repositories
{
    public interface IUserAccounting
    {
        public string Register(RegisterDTO registerDTO);
        public UserToken LogIn(LoginDTO loginDTO);
    }
}
