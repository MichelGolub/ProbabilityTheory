using FluentValidation;

namespace PrTh.Application.Features.Chapters.Commands.DeleteChapter
{
    public class DeleteChapterCommandValidator
        : AbstractValidator<DeleteChapterCommand>
    {
        public DeleteChapterCommandValidator()
        {
            RuleFor(command =>
                command.Id).NotEqual(Guid.Empty);
        }
    }
}
