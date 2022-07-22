using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Notes.Application.Common.Mappings
{
    /// <summary>
    /// Создает конфигурацию автомаппера и ложит ее в конструктор
    /// </summary>
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly assembly) =>
            ApplyMappingsFromAssembly(assembly);

        /// <summary>
        /// Сканирует сборку и ищет любые типы, ктороые реализуют интерфейс IMapWith
        /// Затем вызывает метод mapping от наследонванного типа 
        /// или из интерфейса, если тип не реализует этот метод
        /// </summary>
        /// <param name="assembly"></param>
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType && 
                    i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
                .ToList();
            
            foreach(var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
