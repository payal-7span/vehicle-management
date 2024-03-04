using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VM.Common.Configurations;
using VM.Common.Constants;

namespace VM.Common.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(int userId);
        string GenerateRefreshToken(out Guid tokenId);
        public bool ValidateAccessToken(string accessToken, out int userId);
        public bool ValidateRefreshToken(string refreshToken, out Guid token);
    }

    public class TokenService(IOptions<JWTConfiguration> jwtConfiguration) : ITokenService
    {
        private readonly JWTConfiguration _jwtConfiguration = jwtConfiguration.Value;

        private SecurityKey SymmetricSecurityKey
        {
            get
            {
                var key = Encoding.ASCII.GetBytes(_jwtConfiguration.Secret);
                return new SymmetricSecurityKey(key);
            }
        }

        public bool ValidateAccessToken(string accessToken, out int userId)
        {
            userId = 0;
            if (!ValidateToken(accessToken, GetAccessTokenValidationParameters(), out IEnumerable<Claim> accessTokenClaims))
                return false;

            userId = GetId(accessTokenClaims, ClaimNames.UserId) ?? 0;

            return true;
        }

        public bool ValidateRefreshToken(string refreshToken, out Guid token)
        {
            token = Guid.Empty;

            if (!ValidateToken(refreshToken, GetRefreshTokenValidationParameters(), out IEnumerable<Claim> refreshTokenClaims))
                return false;

            if (Guid.TryParse(GetValue(refreshTokenClaims, JwtRegisteredClaimNames.Jti), out token))
                return true;

            return false;
        }

        private bool ValidateToken(string token, TokenValidationParameters parameters, out IEnumerable<Claim> claims)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            claims = [];
            try
            {
                ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(token, parameters, out SecurityToken validatedToken);
                claims = tokenValid.Claims;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private int? GetId(IEnumerable<Claim> claims, string type)
        {
            string? id = GetValue(claims, type);
            return id == null ? 0 : ParseInt(id);
        }

        private string? GetValue(IEnumerable<Claim> claims, string type)
        {
            return claims.FirstOrDefault(x => x.Type.Equals(type, StringComparison.OrdinalIgnoreCase))?.Value;
        }

        private int ParseInt(string value)
        {
            if (int.TryParse(value, out int result))
                return result;
            return 0;
        }

        public string GenerateAccessToken(int userId)
        {
            var claims = new List<Claim> {
                new(ClaimNames.UserId, userId.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            return GenerateToken(claims, _jwtConfiguration.AccessTokenExpirationMinutes, _jwtConfiguration.ValidIssuer);
        }

        public string GenerateRefreshToken(out Guid tokenId)
        {
            tokenId = Guid.NewGuid();
            var claims = new List<Claim> {
                new(JwtRegisteredClaimNames.Jti, tokenId.ToString()),
            };

            return GenerateToken(claims, _jwtConfiguration.RefreshTokenExpirationMinutes, RefreshTokenIssuer);
        }

        private string GenerateToken(IEnumerable<Claim> claims, int expirationMinutes, string issuer)
        {
            SecurityTokenDescriptor securityTokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
                Audience = _jwtConfiguration.ValidAudience,
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            string token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return token;
        }

        private TokenValidationParameters GetAccessTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidIssuer = _jwtConfiguration.ValidIssuer,
                ValidAudience = _jwtConfiguration.ValidAudience,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false, // Important to not validate the time
                IssuerSigningKey = SymmetricSecurityKey
            };
        }

        private TokenValidationParameters GetRefreshTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidIssuer = RefreshTokenIssuer,
                ValidAudience = _jwtConfiguration.ValidAudience,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = SymmetricSecurityKey
            };
        }

        private string RefreshTokenIssuer => $"{_jwtConfiguration.ValidIssuer}/refresh-token-generator";
    }
}
