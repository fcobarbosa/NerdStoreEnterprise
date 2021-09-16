using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace NSE.WebAPI.Core.Identidade
{
    public static class CustomAuthorization
    {
        public static bool CheckUserClaims(this HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated &&
                context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }
    }

    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(type: typeof(ClaimAuthorizationFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }

    public class ClaimAuthorizationFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public ClaimAuthorizationFilter(Claim claim)
        {
            this._claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            if (!context.HttpContext.CheckUserClaims(this._claim.Type, this._claim.Value))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}
