using Microsoft.AspNetCore.Mvc;
using Shared.Models.Models.Configurations;

namespace Clinicy.Auth.Extensions.ControllerExtensions;

public static class ControllerExtensions
{
    public static void SetAccessAndRefreshCookie(this ControllerBase controllerBase, string accessToken,
        string refreshToken, JwtConfiguration configuration)
    {
        const string refreshCookieKey = "refresh";
        const string accessCookieKey = "access";
        var accessCookieOptions = new CookieOptions
        {
            Secure = true,
            HttpOnly = true,
            SameSite = controllerBase.Request.Headers["User-Agent"].ToString().Contains("Safari")
                ? SameSiteMode.None
                : SameSiteMode.Strict,
            Expires = DateTimeOffset.Now.Add(TimeSpan.FromMinutes(configuration.AccessExpiresIn)),
            MaxAge = TimeSpan.FromMinutes(configuration.AccessExpiresIn),
            Domain = "localhost"
        };
        var refreshCookieOptions = new CookieOptions
        {
            Secure = true,
            HttpOnly = true,
            SameSite = controllerBase.Request.Headers["User-Agent"].ToString().Contains("Safari")
                ? SameSiteMode.None
                : SameSiteMode.Strict,
            Expires = DateTimeOffset.Now.Add(TimeSpan.FromMinutes(configuration.RefreshExpiresIn)),
            MaxAge = TimeSpan.FromMinutes(configuration.RefreshExpiresIn),
            Domain = "localhost"
        };

        controllerBase.Response.Cookies.Append(accessCookieKey, accessToken, accessCookieOptions);
        controllerBase.Response.Cookies.Append(refreshCookieKey, refreshToken, refreshCookieOptions);
    }

    public static string GetRefreshTokenFromCookie(this ControllerBase controllerBase)
    {
        return controllerBase.Request.Cookies["refresh"] ?? "";
    }
}