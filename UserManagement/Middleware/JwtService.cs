using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace UserManagement.Middleware
{
    public interface IJWTHandler
    {
        string GenerateToken(Account account);
        string GetName(string token);
        string GetEmail(string token);
    }
    public class JwtService : IJWTHandler
    {

        private readonly IConfiguration iconfiguration;
        public JwtService(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        public string GenerateToken(Account account)
        {
            if (account == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.PrimarySid,account.ID.ToString()),
                    new Claim(ClaimTypes.Name, account.NAMA_USER),
                    new Claim(ClaimTypes.NameIdentifier, account.USERNAME),
                    new Claim(ClaimTypes.Email, account.EMAIL),
                    new Claim(ClaimTypes.Authentication, account.RoleID.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
        //Buat Generate SideBar
        public string GetName(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken result = tokenHandler.ReadJwtToken(token);

            return result.Claims.FirstOrDefault(claim => claim.Type.Equals("name")).Value;
        }

        public string GetEmail(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken result = tokenHandler.ReadJwtToken(token);

            return result.Claims.FirstOrDefault(claim => claim.Type.Equals("email")).Value;
        }

    }
}
