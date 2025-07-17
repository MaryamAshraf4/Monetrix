using Microsoft.AspNetCore.Identity;
using Monetrix.Models;

namespace Monetrix.MiddleWares
{
    public class ForcePasswordChangeMiddleware
    {
        private readonly RequestDelegate _next;

        public ForcePasswordChangeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<AppUser> userManager)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var user = await userManager.GetUserAsync(context.User);

                if (user != null && user.IsFirstLogin)
                {
                    var path = context.Request.Path.Value.ToLower();

                    if (!path.Contains("/auth/changepassword") && !path.Contains("/auth/logout"))
                    {
                        context.Response.Redirect("/Auth/ChangePassword");
                        return;
                    }
                }
            }
            await _next(context);
        }
    }
}
