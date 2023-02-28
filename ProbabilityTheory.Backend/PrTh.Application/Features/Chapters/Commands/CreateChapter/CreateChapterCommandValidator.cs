using FluentValidation;

namespace PrTh.Application.Features.Chapters.Commands.CreateChapter
{
    public class CreateChapterCommandValidator 
        : AbstractValidator<CreateChapterCommand>
    {
        public CreateChapterCommandValidator()
        {
            RuleFor(command =>
                command.ThemeId).NotEqual(Guid.Empty);
        }
    }
}
