using ECommerce.Application.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Identity.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
        }
        public string CreateToken(string userId, string email, string userName, IReadOnlyList<string> roles)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, userName)
            };

            claims.AddRange(roles.Select(role => new Claim( ClaimTypes.Role , role)));

            var secKey = _jwtSettings.SecretKey;
            if (string.IsNullOrWhiteSpace(secKey))
                throw new InvalidOperationException("Invalid Secret Key");
            if (secKey.Length < 20)
                throw new InvalidOperationException("Secret Key is too Short");

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secKey));

            var cred = new SigningCredentials(Key , SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims : claims,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinuts),
                    signingCredentials: cred
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }

    public class JwtSettings
    {
        public string SecretKey { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public int ExpirationMinuts { get; set; } = default!;
    }
}
