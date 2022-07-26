using System;
using FluentValidation;

namespace Notes.Application.Notes.Commands.CreateNote
{
    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        /// <summary>
        /// задаем правила для создания заметки
        /// </summary>
        public CreateNoteCommandValidator()
        {
            RuleFor(createNoteCommand =>
                createNoteCommand.Title).NotEmpty().MaximumLength(250);
            RuleFor(createNoteCommand =>
                createNoteCommand.UserId).NotEqual(Guid.Empty);
        }
    }
}
