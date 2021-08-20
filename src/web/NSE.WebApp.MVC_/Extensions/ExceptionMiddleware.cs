using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomHttpRequestException e)
            {
                HandleRequestExceptionAsync(httpContext, e);
            }
        }

        private static void HandleRequestExceptionAsync(HttpContext context, CustomHttpRequestException httpRequestException)
        {
            if(httpRequestException.StatusCode == HttpStatusCode.Unauthorized)
            {
                context.Response.Redirect(location: $"/login?RedirectUrl={context.Request.Path}");
                return;
            }
            context.Response.StatusCode = (int)httpRequestException.StatusCode;
        }
    }
}
