using AutoMapper;

namespace Notes.Application.Common.Mappings
{
    /// <summary>
    /// дженерик интерфейс дял типа Т, с реализацией по-умолчанию
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMapWith<T>
    {
        /// <summary>
        /// Создает конфигурацию из исходного типа Т
        /// </summary>
        /// <param name="profile"></param>
        void Mapping(Profile profile) =>
            profile.CreateMap(typeof(T), GetType());
    }
}
