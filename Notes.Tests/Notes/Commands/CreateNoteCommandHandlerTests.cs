using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Tests.Common;
using Xunit;

namespace Notes.Tests.Notes.Commands
{
    /// <summary>
    /// тест создания заметки
    /// </summary>
    public class CreateNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]  //метод должен быть запущен во время прогона теста
        public async Task CreateNoteCommandHandler_Success()
        {
            //Arrange - подготовка данных для теста
            var handler = new CreateNoteCommandHandler(Context);
            var noteName = "note name";
            var noteDetails = "note details";

            //Act
            var noteId = await handler.Handle(
                new CreateNoteCommand
                {
                    Title = noteName,
                    Details = noteDetails,
                    UserId = NotesContextFactory.UserBId
                },
                CancellationToken.None);

            //assert - првоерка результатов
            Assert.NotNull(
                await Context.Notes.SingleOrDefaultAsync(note =>
                note.Id == noteId && note.Title == noteName && note.Details == noteDetails));
        }
    }
}
