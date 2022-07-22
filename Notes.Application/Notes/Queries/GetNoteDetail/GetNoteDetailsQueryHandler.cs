using System;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using Notes.Application.Interfaces;
using Notes.Application.Common.Exceptions;
using Notebook.Domain;

namespace Notes.Application.Notes.Queries.GetNoteDetail
{
    /// <summary>
    /// Обработчик запроса получения заметки
    /// </summary>
    public class GetNoteDetailsQueryHandler  
        : IRequestHandler<GetNoteDetailsQuery, NoteDetailsVm>
    {
        private readonly INotesDBContext _dBContext;
        private readonly IMapper _mapper;
        public GetNoteDetailsQueryHandler(INotesDBContext dbContext, IMapper mapper) =>
            (_dBContext, _mapper) = (dbContext, mapper);
        public async Task<NoteDetailsVm> Handle(GetNoteDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dBContext.Notes
                .FirstOrDefaultAsync(note => note.Id == request.Id, cancellationToken);
            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            return _mapper.Map<NoteDetailsVm>(entity);
        }
    }
}
