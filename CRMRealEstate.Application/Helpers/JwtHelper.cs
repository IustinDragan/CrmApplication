using CRMRealEstate.DataAccess.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRMRealEstate.Application.Helpers;

public class JwtHelper
{
    public static string GenerateToken(Users user, string secretKey)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>
        {
            new("id", user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Roles.ToString())
        };
        //Din appsetings se citeste cu ConfigurationManager
        // if (user.FirstName.Contains("admin"))
        // {
        //     claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        // }

        var claimIdentity = new ClaimsIdentity(claims);

        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = claimIdentity,
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecretKeyMySuperSecretKey2")),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescription);

        return tokenHandler.WriteToken(token);
    }
}