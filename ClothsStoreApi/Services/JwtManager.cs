﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

//https://github.com/cuongle/WebApi.Jwt/blob/master/WebApi.Jwt/JwtManager.cs
namespace ClothsStore.Api.Services
{
    public static class JwtManager
    {
        /// <summary>
        /// Use the below code to generate symmetric Secret Key
        ///     var hmac = new HMACSHA256();
        ///     var key = Convert.ToBase64String(hmac.Key);
        /// </summary>
        private const string Secret = "VExTb0FMaVJaQk56SUNRQzRMb3ByMUtORWFBTEpaWWtzS2hNRHZYOG5vZDk3ZDBtUCtHTFNIcFM3SExzNHJFc3ZOTHhnUGNNMU84TlhUYS9qVDA2NDdEOXhBSGowTnAwTldyVkptOW9aVCtrYkttVTVaVTJZOTA5VmVVaUdQTCtvUU9pZjd5c1RldWNmaGx4ZGtWZjZYRDM4Tlo2V1ZmSllyRXRDUEEwdWUwPQ==";

        public static string GenerateToken(string username, int expireMinutes = 20)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, username)
                        }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
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

                var symmetricKey = Convert.FromBase64String(Secret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

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
