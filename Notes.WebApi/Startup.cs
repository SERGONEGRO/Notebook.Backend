using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Notes.Application;
using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Persistance;
using Notes.WebApi.Middleware;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Notes.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

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

            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer("Bearer", options =>
                 {
                     options.Authority = "https://localhost:7214";
                     options.Audience = "notesWebApi";
                     options.RequireHttpsMetadata = false;
                 });

            services.AddVersionedApiExplorer(options =>
                options.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>,
                ConfigureSwaggerOptions>();

            //добавляем сваггер
            services.AddSwaggerGen(config =>
            {   //настраиваем использование xml-файла
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });

            services.AddApiVersioning();  //добавляем версионирование
        }

        /// <summary>
        /// Здесь настраивается конвейер обработки запроса. Применяются все middleware
        /// выполняются в том порядке, в ктором добавляются в конвейер
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {   //задаем адрес свагера по умолчанию в зависимости от версий API
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    config.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                    config.RoutePrefix = String.Empty;
                }

                //задаем адрес свагера по умолчанию
                //config.RoutePrefix = String.Empty;
                //config.SwaggerEndpoint("swagger/v1/swagger.json", "Notes Api");
            });
            app.UseCustomExceptionHandler(); //добавляем наш middleware
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            //версионирование
            app.UseApiVersioning();
            //роутинг мапится на названия контроллеров и их методы
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
