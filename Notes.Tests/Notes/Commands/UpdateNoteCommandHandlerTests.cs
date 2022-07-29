using System;
using System.Threading;
using System.Threading.Tasks;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Common.Exceptions;
using Notes.Tests.Common;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace Notes.Tests.Notes.Commands
{
    public class UpdateNoteCommandHandlerTests : TestCommandBase
    {
        /// <summary>
        /// проверяет факт обновления
        /// </summary>
        /// <returns></returns>
        [Fact]  //метод должен быть запущен во время прогона теста
        public async Task UpdateNoteCommandHandler_Success()
        {
            //Arrange
            var handler = new UpdateNoteCommandHandler(Context);
            var updatedTitle = "new title";

            //Act
            await handler.Handle(new UpdateNoteCommand
            {
                Id = NotesContextFactory.NoteIdForUpdate,
                UserId = NotesContextFactory.UserBId,
                Title = updatedTitle
            },
                CancellationToken.None);

            //assert - првоерка результатов
            Assert.NotNull(
                await Context.Notes.SingleOrDefaultAsync(note =>
                note.Id == NotesContextFactory.NoteIdForUpdate &&
                note.Title == updatedTitle));
        }

        /// <summary>
        /// проверяет логику, падает при неверном id
        /// </summary>
        /// <returns></returns>
        [Fact]  //метод должен быть запущен во время прогона теста
        public async Task UpdateNoteCommandHandler_FailOnWrongId()
        {
            //Arrange - подготовка данных для теста
            var handler = new UpdateNoteCommandHandler(Context);

            //Act
            //assert - првоерка результатов
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                     new UpdateNoteCommand
                     {
                         Id = Guid.NewGuid(),
                         UserId = NotesContextFactory.UserAId
                     },
                     CancellationToken.None));
        }

        /// <summary>
        /// проверяет логику, что id пользователя = id польльзователя в заметке
        /// </summary>
        /// <returns></returns>
        [Fact]  //метод должен быть запущен во время прогона теста
        public async Task UpdateNoteCommandHandler_FailOnWrongUserId()
        {
            //Arrange - подготовка данных для теста
            var updateHandler = new UpdateNoteCommandHandler(Context);

            //Act
            //assert - првоерка результатов
            await Assert.ThrowsAsync<NotFoundException>(async () =>
               await updateHandler.Handle(
                    new UpdateNoteCommand
                    {
                        Id = NotesContextFactory.NoteIdForUpdate,
                        UserId = NotesContextFactory.UserAId
                    },
                    CancellationToken.None));
        }
    }
}
