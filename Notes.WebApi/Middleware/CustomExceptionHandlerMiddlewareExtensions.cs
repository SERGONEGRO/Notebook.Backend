using Microsoft.AspNetCore.Builder;
namespace Notes.WebApi.Middleware
{
    /// <summary>
    /// Расширения, чтобы включать middleware в конвейер
    /// </summary>
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
