using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Notebook.Domain;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;

namespace Notes.Application.Notes.Commands.DeleteCommand;

/// <summary>
/// Обработчик команды удаления заметки
/// </summary>
public class DeleteNoteCommandHandler
    : IRequestHandler<DeleteNoteCommand>
{
    private readonly INotesDBContext _dbContext;
    public DeleteNoteCommandHandler(INotesDBContext dBContext) =>
        _dbContext = dBContext;

    /// <summary>
    /// Удаляет запись
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<Unit> Handle(DeleteNoteCommand request, //Unit -тип, обозначающий пустой ответ
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Notes
            .FindAsync(new object[] { request.Id},cancellationToken);

        if (entity == null || entity.UserId != request.UserId)
        {
            throw new NotFoundException(nameof(Note), request.Id);
        }

        _dbContext.Notes.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
