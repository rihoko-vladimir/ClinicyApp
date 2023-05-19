using Clinicy.WebApi.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Clinicy.WebApi.CustomFilters;

public class RoleCheck : ActionFilterAttribute
{
    private readonly string _role;

    public RoleCheck(string role)
    {
        _role = role;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var accessToken = await context.HttpContext.GetTokenAsync("access_token");
        var accessTokenService = context.HttpContext.RequestServices.GetService<IAccessTokenService>();
        accessTokenService!.GetRoleFromAccessToken(accessToken!, out var role);

        if (role != _role)
        {
            context.Result = new ForbidResult();
        }
    }
}