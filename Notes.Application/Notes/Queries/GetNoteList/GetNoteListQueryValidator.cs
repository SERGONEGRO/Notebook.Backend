using System;
using FluentValidation;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class GetNoteListQueryValidator : AbstractValidator<GetNoteListQuery>
    {
        /// <summary>
        /// задаем правила для получения списка заметок
        /// </summary>
        public GetNoteListQueryValidator()
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        }
    }
}
