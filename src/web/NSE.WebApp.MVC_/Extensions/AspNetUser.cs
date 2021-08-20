using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace NSE.WebApp.MVC.Extensions
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _acessor;
        public AspNetUser(IHttpContextAccessor acessor)
        {
            this._acessor = acessor;
        }
        public string Name => this._acessor.HttpContext.User.Identity.Name;

        public bool EstaAutenticado()
        {
            return this._acessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> ObterClaims()
        {
            return this._acessor.HttpContext.User.Claims;
        }

        public HttpContext ObterContext()
        {
            return this._acessor.HttpContext;
        }

        public string ObterUserEmail()
        {
            return EstaAutenticado() ? this._acessor.HttpContext.User.GetUserEmail() : string.Empty;
        }

        public Guid ObterUserId()
        {
            return EstaAutenticado() ? Guid.Parse( this._acessor.HttpContext.User.GetUserId()) : Guid.Empty;
        }

        public string ObterUserToken()
        {
            return EstaAutenticado() ? this._acessor.HttpContext.User.GetUserToken() : string.Empty;
        }

        public bool PossuiRole(string role)
        {
            return this._acessor.HttpContext.User.IsInRole(role);
        }
    }
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            return principal.GetClaim("sub");
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            return principal.GetClaim("email");
        }

        public static string GetUserToken(this ClaimsPrincipal principal)
        {
            return principal.GetClaim("JWT");
        }

        private static string GetClaim(this ClaimsPrincipal principal, string name)
        {
            if (principal == null) throw new ArgumentException(nameof(principal));
            var claim = principal.FindFirst(type: name);
            return claim?.Value;
        }
    }
}
