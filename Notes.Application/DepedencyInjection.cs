using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Notes.Application
{
    /// <summary>
    /// регистрирует Медиатр с помощью специального метода addMediatr
    /// </summary>
    public static class DepedencyInjection
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
