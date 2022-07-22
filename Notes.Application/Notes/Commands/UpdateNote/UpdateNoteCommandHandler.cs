using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using Notes.Application.Common.Exceptions;
using Notebook.Domain;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    /// <summary>
    /// обработчик команды обновления заметки
    /// </summary>
    public class UpdateNoteCommandHandler
        :IRequestHandler<UpdateNoteCommand>
    {
        private readonly INotesDBContext _dBContext;
        public UpdateNoteCommandHandler(INotesDBContext dbContext) =>
            _dBContext = dbContext;

        /// <summary>
        /// Метод обновляющий заявку
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<Unit> Handle(UpdateNoteCommand request,
            CancellationToken cancellationToken)
        {
            //ищет сущность по id
            var entity =
                await _dBContext.Notes.FirstOrDefaultAsync(note =>
                    note.Id == request.Id, cancellationToken);
            //если не найдено - выдает наше исключение
            if(entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }
            //если найдена - обновляем и сохраняем в контекст БД
            entity.Details = request.Details;
            entity.Title = request.Title;
            entity.EditDate = DateTime.Now;

            await _dBContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
