using Core.Entities;
using Infrastructure.Commons;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Extensions
{
    public static class JwtExtensions
    {
        private static AppConfig _appConfig;

        public static void Configure(IConfiguration configuration)
        {
            _appConfig = configuration.GetSection(nameof(AppConfig)).Get<AppConfig>();

            if (_appConfig == null)
                throw new InvalidOperationException("AppConfig is not properly configured.");
        }

        public static string GenerateJwtToken(this User user, IList<Claim> userClaims)
        {
            EnsureAppConfigIsInitialized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appConfig.JwtConfig.SigningKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClaims),
                Expires = DateTime.UtcNow.AddMinutes(_appConfig.JwtConfig.TokenExpirationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                //Issuer = _appConfig.JwtConfig.Issuer,
                //Audience = _appConfig.JwtConfig.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public static ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            EnsureAppConfigIsInitialized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appConfig.JwtConfig.SigningKey);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        // Ensures that AppConfig is initialized
        private static void EnsureAppConfigIsInitialized()
        {
            if (_appConfig == null)
            {
                throw new InvalidOperationException("AppConfig is not initialized. Ensure that JwtExtensions.Configure has been called.");
            }
        }
    }
}
