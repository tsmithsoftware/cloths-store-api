using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

//https://github.com/cuongle/WebApi.Jwt/blob/master/WebApi.Jwt/JwtManager.cs
//https://stackoverflow.com/questions/29754662/signatureverificationfailedexception-in-jwtauthforwebapi
namespace ClothsStore.Api.Services
{
    public static class JwtManager
    {
        /// <summary>
        /// Use the below code to generate symmetric Secret Key
        ///     var hmac = new HMACSHA256();
        ///     var key = Convert.ToBase64String(hmac.Key);
        /// </summary>
        private const string Secret = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwczovL3NlY3VyZS5leGFtcGxlLmNvbS8iLCJleHAiOjE0MTA4MTkzODAsImh0dHA6Ly9leGFtcGxlLmNvbS9vcmdudW0iOiI5ODc5ODc5ODciLCJodHRwOi8vZXhhbXBsZS5jb20vdXNlciI6Im1lQGV4YW1wbGUuY29tIiwiaWF0IjoxNDA4NDE5NTQwfQ.jW9KChUTcgXMDp5CnTiXovtQZsN4X-M-V6_4rzu8Zk8";
        private const string Issuer = "localhost";

        public static string GenerateToken(string username, int expireMinutes = 20)
        {
            var varSecret = Secret;
            //var symmetricKey = Convert.ToBase64String(Encoding.ASCII.GetBytes(varSecret));//Convert.FromBase64String(Secret);

            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                    new Claim(ClaimTypes.Name, username)
                }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(getSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature),

                Issuer = "localhost",

                Audience = "localhost"

            };

        SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }

    private static SecurityKey getSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Convert.ToBase64String(Encoding.ASCII.GetBytes(Secret))));
    }

    internal static Task<bool> ValidateTokenAsync(string parameter)
        {
            Console.Write("Validating Token: " + parameter);
            var principal = GetPrincipal(parameter);
            Console.Write("principal:" + principal.Identity.Name);
            return Task.FromResult(principal != null);
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var validationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateActor = false,
                    ValidateLifetime = false,
                    ValidIssuer = Issuer,
                    ValidAudience = Issuer,
                    IssuerSigningKey = getSymmetricSecurityKey()
                };

                IdentityModelEventSource.ShowPII = true;

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

                return principal;
            }

            catch (Exception)
            {
                return null;
            }
        }
    }
}
