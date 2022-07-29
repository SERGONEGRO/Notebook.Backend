using System;
using AutoMapper;
using Notes.Application.Interfaces;
using Notes.Application.Common.Mappings;
using Notes.Persistance;
using Xunit;

namespace Notes.Tests.Common
{
    /// <summary>
    /// Вспомогательный класс ,создающий маппер, который принимает передастся getNoteListQueryHandlerTestу
    /// </summary>
    public class QueryTestFixture : IDisposable
    {
        public NotesDbContext Context;
        public IMapper Mapper;

        public QueryTestFixture()
        {
            Context = NotesContextFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(typeof(INotesDBContext).Assembly));
            });
            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            NotesContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
