using MediatR;
using System;
using System.Threading;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    /// <summary>
    /// обработчик запроса списка заметок
    /// </summary>
    public class GetNoteListQueryHandler
        : IRequestHandler<GetNoteListQuery, NoteListVm>
    {
        private readonly INotesDBContext _dBContext;
        private readonly IMapper _mapper;
        public GetNoteListQueryHandler(INotesDBContext dbContext, IMapper mapper) =>
           (_dBContext, _mapper) = (dbContext, mapper);
        public async Task<NoteListVm> Handle(GetNoteListQuery request, CancellationToken cancellationToken)
        {
            var notesQuery = await _dBContext.Notes
                .Where(note => note.UserId == request.UserId)
                //используем метод расширения в библиотеке AutoMapper, который проецирует
                //нашу коллекцию в соответсвии с заданной конфигурацией
                .ProjectTo<NoteLookupDto>(_mapper.ConfigurationProvider) 
                .ToListAsync(cancellationToken);

            return new NoteListVm { Notes = notesQuery };
        }
    }
}
