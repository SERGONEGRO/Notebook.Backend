using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Notes.Application.Interfaces;

namespace Notes.Persistance
{
    public static class DepedencyInjection
    {
        /// <summary>
        /// Метод расширения добавляет контекст БД и регистрирует его
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddPersistance(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<NotesDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            //Создает экземпляр объекта на время существования запроса
            services.AddScoped<INotesDBContext>(provider => provider.GetService<NotesDbContext>());

            return services;
        }
    }
}
