using System;
using FluentValidation;

namespace Notes.Application.Notes.Queries.GetNoteDetail
{
    public class GetNoteDetailQueryValidator : AbstractValidator<GetNoteDetailsQuery>
    {
        /// <summary>
        /// задаем правила для получения заметки
        /// </summary>
        public GetNoteDetailQueryValidator()
        {
            RuleFor(note => note.Id).NotEqual(Guid.Empty);
            RuleFor(note => note.UserId).NotEqual(Guid.Empty);
        }
        
    }
}
