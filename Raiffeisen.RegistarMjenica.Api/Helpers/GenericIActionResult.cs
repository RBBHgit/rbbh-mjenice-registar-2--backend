using Microsoft.AspNetCore.Mvc;

namespace Raiffeisen.RegistarMjenica.Api.Helpers;

public static class GenericIActionResult<TStatusCode, TData> where TStatusCode : struct where TData : class
{
    public static IActionResult CreateStatusCodeResult(int statusCode, TData data = null)
    {
        return statusCode switch
        {
            200 => new OkObjectResult(data),
            500 => new BadRequestObjectResult(data) { StatusCode = statusCode },
            _ => new ObjectResult(data) { StatusCode = statusCode }
        };
    }
}