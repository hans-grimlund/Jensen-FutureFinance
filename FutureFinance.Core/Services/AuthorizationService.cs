using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FutureFinance.Domain;
using Microsoft.IdentityModel.Tokens;

namespace FutureFinance.Core;

public class AuthorizationService : IAuthorizationService
{
    public string GenerateToken(UserEntity user)
    {
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenKey.key)),
                SecurityAlgorithms.HmacSha256);
                
            List<Claim> claims = [];

            if (user.Role == 1)
                claims.Add(new Claim(ClaimTypes.Role, "Standard"));
            if (user.Role == 2)
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));

            claims.Add(new Claim("UserId", user.Id.ToString()));

            var token = new JwtSecurityToken(
                issuer: "http://localhost:5230",
                audience: "http://localhost:5230",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
