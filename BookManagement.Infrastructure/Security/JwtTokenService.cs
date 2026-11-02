using BookManagement.Application.Configuration;
using BookManagement.Application.DTOs.Security;
using BookManagement.Application.Interfaces.Security;
using BookManagement.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookManagement.Infrastructure.Security
{
    public class JwtTokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenService(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
        }

        public TokenResult GenerateAccessToken(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            if (user.Role is null)
            {
                throw new ArgumentException("User role must be loaded before generating a token.", nameof(user));
            }

            var secretKeyBytes = Convert.FromBase64String(_jwtSettings.SecretKey);

            var securityKey = new SymmetricSecurityKey(secretKeyBytes);

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("must_change_password", user.MustChangePassword.ToString().ToLowerInvariant())
            };

            var expiresAt = DateTimeOffset.UtcNow.AddMinutes(
                _jwtSettings.ExpirationInMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expiresAt.UtcDateTime,
                signingCredentials: signingCredentials);

            var tokenHandler = new JwtSecurityTokenHandler();

            var accessToken = tokenHandler.WriteToken(token);

            return new TokenResult
            {
                AccessToken = accessToken,
                ExpiresAt = expiresAt
            };
        }
    }
}
