using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Clinicy.WebApi.Extensions.ControllerExtensions;

public static class ControllerExtensions
{
    //Метод-расширение для контроллера. Получает токен из куков. Повторяется в других сервисах.
    public static async Task<string> GetAccessTokenAsync(this ControllerBase controllerBase)
    {
        return (await controllerBase.HttpContext.GetTokenAsync("access_token"))!;
    }
}