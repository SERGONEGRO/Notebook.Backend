using System;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.InMemory;
using Notebook.Domain;
using Notes.Persistance;

namespace Notes.Tests.Common
{
    /// <summary>
    /// Создает контексты данных. Используется, чтобы не зависеть от БД
    /// </summary>
    public class NotesContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid NoteIdForDelete = Guid.NewGuid();
        public static Guid NoteIdForUpdate = Guid.NewGuid();

        public static NotesDbContext Create()
        {
            var options = new DbContextOptionsBuilder<NotesDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())  //используем InMemorydatabase
                .Options;

            var context = new NotesDbContext(options);
            context.Database.EnsureCreated();
            context.Notes.AddRange( //добавляем тестовую заметку
                new Note
                {
                    CreationDate = DateTime.Today,
                    Details = "Details1",
                    EditDate = null,
                    Id = Guid.Parse("2AE75F2F-3004-4A3E-AF4B-2A69FC190367"),
                    Title = "Title1",
                    UserId = UserAId
                },
                 new Note
                 {
                     CreationDate = DateTime.Today,
                     Details = "Details2",
                     EditDate = null,
                     Id = Guid.Parse("EC420A64-B8BE-4987-B942-AF3E0110C991"),
                     Title = "Title2",
                     UserId = UserBId
                 },
                 new Note
                 {
                     CreationDate = DateTime.Today,
                     Details = "Details3",
                     EditDate = null,
                     Id = NoteIdForDelete,
                     Title = "Title3",
                     UserId = UserAId
                 },
                 new Note
                 {
                     CreationDate = DateTime.Today,
                     Details = "Details4",
                     EditDate = null,
                     Id = NoteIdForUpdate,
                     Title = "Title4",
                     UserId = UserBId
                 }
            );
            context.SaveChanges();
            return context;
        }

        /// <summary>
        /// убеждаемся, что база удалена
        /// </summary>
        public static void Destroy(NotesDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
