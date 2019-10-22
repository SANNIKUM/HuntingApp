
namespace AFFHA.API.Application.Middlewares
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using AFFHA.Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Primitives;

    public static class UseApiKeyValidationExtensions
    {
        public static IApplicationBuilder UseApiKeyValidation(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiKeyValidationMiddleware>();
        }
    }

    public class ApiKeyValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiKeyValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, AFFHADbContext db, IConfiguration config)
        {
            var routePrefix = config.GetValue("AppSettings:PublicPath", "")
                .TrimEnd('/');

            if (context.Request.Path.StartsWithSegments(routePrefix + "/apps")
                || context.Request.Path.StartsWithSegments(routePrefix + "/swagger"))
            {
                await _next(context);
            }
            else
            {
                string appId = context.Request.Headers["x-app-id"];
                string apiKey = context.Request.Headers["x-api-key"];

                if (string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(apiKey))
                {
                    await HandleChallengeAsync(context);
                    return;
                }

                var application = await db.Applications.FirstOrDefaultAsync(a => a.AppId == appId);

                if (application == null)
                {
                    await HandleChallengeAsync(context);
                    return;
                }

                using (var hmac = new HMACSHA512(application.ApiKeySalt))
                {
                    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(apiKey));

                    if (!computedHash.SequenceEqual(application.ApiKeyHash))
                    {
                        await HandleChallengeAsync(context);
                        return;
                    }
                    else
                    {
                        await _next(context);
                    }
                }
            }
        }

        private async Task HandleChallengeAsync(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("Permission Denied");
        }
    }
}
