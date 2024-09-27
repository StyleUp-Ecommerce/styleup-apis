//using Core.Entities;
//using Infrastructure.Commons;
//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;

//namespace Infrastructure.Extensions
//{
//    public static class JwtExtensions
//    {
//        private static AppConfig _options;

//        public static void Configure(IOptions<AppConfig> options)
//        {
//            _options = options.Value;
//        }

//        public static string GenerateJwtToken(this User user, IList<Claim> userClaims)
//        {
//            EnsureAppConfigIsInitialized();

//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.ASCII.GetBytes(_options.JwtConfig.SigningKey);

//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(userClaims),
//                Expires = DateTime.UtcNow.AddMinutes(_options.JwtConfig.TokenExpirationInMinutes),
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
//                Issuer = _options.JwtConfig.Issuer,
//                Audience = _options.JwtConfig.Audience
//            };

//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            return tokenHandler.WriteToken(token);
//        }

//        public static string GenerateRefreshToken()
//        {
//            var randomNumber = new byte[32];
//            using (var rng = RandomNumberGenerator.Create())
//            {
//                rng.GetBytes(randomNumber);
//                return Convert.ToBase64String(randomNumber);
//            }
//        }

//        public static ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
//        {
//            EnsureAppConfigIsInitialized();

//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.ASCII.GetBytes(_options.JwtConfig.SigningKey);

//            var tokenValidationParameters = new TokenValidationParameters
//            {
//                ValidateAudience = false,
//                ValidateIssuer = false,
//                ValidateIssuerSigningKey = true,
//                IssuerSigningKey = new SymmetricSecurityKey(key),
//                ValidateLifetime = false // Token hết hạn vẫn được validate
//            };

//            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

//            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
//            {
//                throw new SecurityTokenException("Invalid token");
//            }

//            return principal;
//        }

//        // Helper method to ensure _options is initialized
//        private static void EnsureAppConfigIsInitialized()
//        {
//            if (_options == null)
//            {
//                throw new InvalidOperationException("JWT options have not been configured. Call JwtExtensions.Configure first.");
//            }
//        }
//    }
//}
