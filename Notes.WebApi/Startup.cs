﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Notes.Application;
using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Persistance;

namespace Notes.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Добавление всех сервисов, которые планируется использовать в приложении
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //конфигурируем автомаппер здесь, а не в notes.application, потому что нужно получить информацию
            //о текущей сборке
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(INotesDBContext).Assembly));
            });

            services.AddAplication();
            services.AddPersistance(Configuration);
            services.AddControllers();

            //CORS - cross-origin resource sharing - технология современных браузеров по своместному использованию ресурсов
            //здесь мы разрешаем любой доступ, но в реальном приложении нужно ограничивать
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });                 
            });
        }

        /// <summary>
        /// Здесь настраивается конвейер обработки запроса. Применяются все middleware
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           if (env.IsDevelopment())
           {
                app.UseDeveloperExceptionPage();
           }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");


            //роутинг мапится на названия контроллеров и их методы
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}