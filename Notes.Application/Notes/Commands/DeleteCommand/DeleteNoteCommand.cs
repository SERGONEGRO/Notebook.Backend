using System;
using MediatR;

namespace Notes.Application.Notes.Commands.DeleteCommand;

/// <summary>
/// команда удаления заметки
/// </summary>
public class DeleteNoteCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
}
