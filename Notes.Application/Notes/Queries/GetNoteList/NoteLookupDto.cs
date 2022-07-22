using System;
using Notes.Application.Common.Mappings;
using Notebook.Domain;
using AutoMapper;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    /// <summary>
    /// описывает элемент списка заметок. Каждый элемент содержит только ту информацию, 
    /// которая необходима списку заметок
    /// </summary>
    public class NoteLookupDto: IMapWith<Note>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Note, NoteLookupDto>()
                 .ForMember(noteDto => noteDto.Id,
                    opt => opt.MapFrom(note => note.Id))
                 .ForMember(noteDto => noteDto.Title,
                    opt => opt.MapFrom(note => note.Title));
        }
    }
}
