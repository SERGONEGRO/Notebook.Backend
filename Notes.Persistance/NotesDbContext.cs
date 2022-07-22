using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using Notebook.Domain;
using Notes.Persistance.EntityTypeConfigurations;

namespace Notes.Persistance
{
    public class NotesDbContext : DbContext, INotesDBContext
    {
        public DbSet<Note> Notes { get; set; }
        public NotesDbContext(DbContextOptions<NotesDbContext> options)
            : base(options) { }

        /// <summary>
        /// переопределим этот метод, чтобы сообщить EF информация о конфигурации наших сущностей
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new NoteConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
