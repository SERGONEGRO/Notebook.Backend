using System;
using FluentValidation;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
    {
        /// <summary>
        /// задаем правила для обновления заметки
        /// </summary>
        public UpdateNoteCommandValidator()
        {
            RuleFor(updateNoteCommand =>
                updateNoteCommand.Id).NotEqual(Guid.Empty);
            RuleFor(updateNoteCommand =>
                updateNoteCommand.Title).NotEmpty().MaximumLength(250);
            RuleFor(updateNoteCommand =>
                updateNoteCommand.UserId).NotEqual(Guid.Empty);
        }
    }
}
