using MediatR;
using System;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    /// <summary>
    /// Запрос списка заметок
    /// </summary>
    public class GetNoteListQuery : IRequest<NoteListVm>
    {
        public Guid UserId { get; set; }
    }
}
