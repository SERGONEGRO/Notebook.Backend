using System;
using MediatR;

namespace Notes.Application.Notes.Queries.GetNoteDetail
{
    /// <summary>
    /// используем маппинг
    /// </summary>
    public class GetNoteDetailsQuery : IRequest<NoteDetailsVm>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
