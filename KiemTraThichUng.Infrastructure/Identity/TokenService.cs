// File: KiemTraThichUng.Infrastructure/Identity/TokenService.cs
using KiemTraThichUng.Application.Abstractions.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KiemTraThichUng.Infrastructure.Identity
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _jwtOptions;

        public TokenService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateAccessToken(
            int userId,
            string username,
            IEnumerable<string>? roles = null)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Name, username)
        };

            if (roles != null)
            {
                claims.AddRange(roles.Select(role =>
                    new Claim(ClaimTypes.Role, role)));
            }

            var keyBytes = Convert.FromBase64String(_jwtOptions.Key);

            var key = new SymmetricSecurityKey(keyBytes);

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
