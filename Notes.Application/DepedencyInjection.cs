using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Common.Behaviors;

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
            
            //добавляем валидатор из сборки
            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });

            //регистрируем IPipelineBehavior
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
