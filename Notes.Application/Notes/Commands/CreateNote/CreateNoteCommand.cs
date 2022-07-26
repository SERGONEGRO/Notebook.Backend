using System;
using MediatR;

namespace Notes.Application.Notes.Commands.CreateNote
{
    /// <summary>
    /// Содержит информацию о том, что необходимо для создания заметки
    /// Помечает результат выполнения команды и возвращает результат определенного типа
    /// </summary>
    public class CreateNoteCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
