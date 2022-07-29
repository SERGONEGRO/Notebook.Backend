using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Notes.Application.Notes.Commands.DeleteCommand;
using Notes.Application.Common.Exceptions;
using Notes.Tests.Common;
using Xunit;
using System;
using Notes.Application.Notes.Commands.CreateNote;

namespace Notes.Tests.Notes.Commands
{
    public class DeleteNoteCommandHandlerTests : TestCommandBase
    {
        /// <summary>
        /// проверяет логику 
        /// </summary>
        /// <returns></returns>
        [Fact]  //метод должен быть запущен во время прогона теста
        public async Task DeleteNoteCommandHandler_Success()
        {
            //Arrange - подготовка данных для теста
            var handler = new DeleteNoteCommandHandler(Context);

            //Act
            await handler.Handle(new DeleteNoteCommand
                {
                    Id = NotesContextFactory.NoteIdForDelete,
                    UserId = NotesContextFactory.UserAId
                },
                CancellationToken.None);

            //assert - првоерка результатов
            Assert.Null(Context.Notes.SingleOrDefault(note =>
                note.Id == NotesContextFactory.NoteIdForDelete));
        }

        /// <summary>
        /// проверяет логику, что id верен
        /// </summary>
        /// <returns></returns>
        [Fact]  //метод должен быть запущен во время прогона теста
        public async Task DeleteNoteCommandHandler_FailOnWrongId()
        {
            //Arrange - подготовка данных для теста
            var handler = new DeleteNoteCommandHandler(Context);

            //Act
            //assert - првоерка результатов
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                     new DeleteNoteCommand
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
        public async Task DeleteNoteCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var deleteHandler = new DeleteNoteCommandHandler(Context);
            var createHandler = new CreateNoteCommandHandler(Context);
            var noteId = await createHandler.Handle(
                new CreateNoteCommand
                {
                    Title = "NoteTitle",
                    UserId = NotesContextFactory.UserAId,
                    Details = "NoteDetails"
                }, CancellationToken.None);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await deleteHandler.Handle(
                    new DeleteNoteCommand
                    {
                        Id = noteId,
                        UserId = NotesContextFactory.UserBId
                    }, CancellationToken.None));
        }
    }
}
