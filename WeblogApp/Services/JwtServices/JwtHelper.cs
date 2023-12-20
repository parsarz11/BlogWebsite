using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WeblogApp.Services.JwtServices
{
    public static class JwtHelper
    {
        public static Claim[] GetClaims(this UserToken userAccounts)
        {
            //IEnumerable<Claim> claims = new Claim[] {


            //        new Claim(ClaimTypes.Email, userAccounts.EmailId),
            //        new Claim(ClaimTypes.NameIdentifier, userAccounts.UserName),
            //        new Claim(ClaimTypes.Role, userAccounts.Role)
            //};

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, userAccounts.EmailId),
                new Claim(ClaimTypes.NameIdentifier, userAccounts.UserName),
                new Claim(ClaimTypes.Role, userAccounts.Role)
            };
            return claims;
        }

        //public static IEnumerable<Claim> GetClaims(this UserToken userAccounts, out str Id)
        //{
        //    Id = "";
        //    return GetClaims(userAccounts, Id);
        //}


        public static UserToken GenTokenkey(UserToken model, JwtSettings jwtSettings)
        {
            try
            {
                var UserToken = new UserToken();
                if (model == null) throw new ArgumentException(nameof(model));
                // Get secret key
                var key = Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
                Guid Id = Guid.Empty;
                DateTime expireTime = DateTime.UtcNow.AddDays(10);
                UserToken.Validaty = expireTime.TimeOfDay;
                //----
                var JWToken = new JwtSecurityToken
                    (
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: model.GetClaims(),
                    expires: new DateTimeOffset(expireTime).DateTime,
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
                //----

                //var JWToken = new JwtSecurityToken(issuer: jwtSettings.ValidIssuer, audience: jwtSettings.ValidAudience, notBefore: new DateTimeOffset(DateTime.Now).DateTime, expires: new DateTimeOffset(expireTime).DateTime, signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
                UserToken.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                UserToken.UserName = model.UserName;
                UserToken.Id = model.Id;
                UserToken.GuidId = Id;
                UserToken.Role = model.Role;
                return UserToken;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
