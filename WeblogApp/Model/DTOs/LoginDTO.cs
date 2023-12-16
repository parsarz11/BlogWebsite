using Microsoft.AspNetCore.Identity;

namespace WeblogApp.Model.DTOs
{
    public class LoginDTO
    {

        public string UserName { get; set; }

        public string Password { get; set; }
        public bool IsPersistent { get; set; } = false;
    }
}
