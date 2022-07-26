using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Notes.Application.Interfaces;
using Notebook.Domain;

namespace Notes.Application.Notes.Commands.CreateNote
{
    /// <summary>
    /// Обработчик создания команды
    /// на основе информации, необходимой для создания заметки, содержит логику создания
    /// указываем ему тип запроса и тип ответа
    /// </summary>
    public class CreateNoteCommandHandler
        :IRequestHandler<CreateNoteCommand, Guid>
    {
        private readonly INotesDBContext _dbContext;
        public CreateNoteCommandHandler(INotesDBContext dBContext) => _dbContext = dBContext;
        public async Task<Guid> Handle (CreateNoteCommand request,
            CancellationToken cancellationToken)
        {
            var note = new Note
            {
                UserId = request.UserId,
                Title = request.Title,
                Details = request.Details,
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                EditDate = null
            };

            await _dbContext.Notes.AddAsync(note, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return note.Id;
        }
    }
}
