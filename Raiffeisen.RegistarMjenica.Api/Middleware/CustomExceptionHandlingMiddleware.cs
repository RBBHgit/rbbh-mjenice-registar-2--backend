using Raiffeisen.RegistarMjenica.Services.Exceptions;

namespace Raiffeisen.RegistarMjenica.Api.Middleware;

public class CustomExceptionHandlingMiddleware
{
    private readonly ILogger<CustomExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public CustomExceptionHandlingMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var propertyErrors = new Dictionary<string, string>();

        try
        {
            await _next(context);
        }
        catch (CustomDataAccessException ex)
        {
            var logId = 0;
            //dodati servis za logiranje i zalogirati stacktrace, logid..

            var problemDetails = new CustomProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Greška na serveru",
                Detail = ex.Message,
                LogId = logId,
                Type = ex.ServiceType
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred.");

            // Create a ProblemDetails instance for the error response
            var problemDetails = new CustomProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An unexpected error occurred.",
                Detail = ex.Message
            };

            // Serialize the ProblemDetails to JSON and write it to the response
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}