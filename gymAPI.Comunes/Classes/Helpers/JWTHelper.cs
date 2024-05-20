using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace gymAPI.Comunes.Classes.Helpers
{
    public class JWTHelper
    {
        public static string GenerarToken(string nombre, IConfiguration _config)
        {
            //pyaload
            var claims = new []{
                new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserName", nombre)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpireTime"].ToString())),
                signingCredentials: signIn);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}