using ClothsStore.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace ClothsStore.Api.Filters
{
    public class TokenAuthenticate : Attribute, IAuthenticationFilter
    {

        public bool AllowMultiple
        {
            get
            {
                return false;
            }
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            try
            {
                
                HttpRequestMessage request = context.Request;
                AuthenticationHeaderValue authorization = request.Headers.Authorization;

                if (authorization == null)
                {
                    context.ErrorResult = new AuthenticationFailureResult("Missing autorization header", request);
                    return;
                }
                if (authorization.Scheme != "Bearer")
                {
                    context.ErrorResult = new AuthenticationFailureResult("Invalid autorization scheme", request);
                    return;
                }
                if (String.IsNullOrEmpty(authorization.Parameter))
                {
                    context.ErrorResult = new AuthenticationFailureResult("Missing Token", request);
                    return;
                }

                Boolean correctToken = await JwtManager.ValidateTokenAsync(authorization.Parameter);
                if (!correctToken)
                    context.ErrorResult = new AuthenticationFailureResult("Invalid Token", request);

                var principal = JwtManager.GetPrincipal(authorization.Parameter);

                /**List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, "Eddie Admin"),
                    new Claim(ClaimTypes.Role, "Admin"),
                    // new Claim(ClaimTypes.Role, "Delete"),
                };

                // create an identity with the valid claims.
                ClaimsIdentity identity = new ClaimsIdentity(claims, "yourScheme");

                // set the context principal.
                context.Principal = new ClaimsPrincipal(new[] { identity });**/
                await Task.FromResult(principal != null);
            }
            catch (Exception ex)
            {
                context.ErrorResult = new AuthenticationFailureResult("Exception: \n" + ex.Message, context.Request);
            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(false);
        }
    }
}
