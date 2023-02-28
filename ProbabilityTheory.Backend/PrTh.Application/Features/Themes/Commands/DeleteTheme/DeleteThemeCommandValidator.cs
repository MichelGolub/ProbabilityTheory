using FluentValidation;

namespace PrTh.Application.Features.Themes.Commands.DeleteTheme
{
    public class DeleteThemeCommandValidator 
        : AbstractValidator<DeleteThemeCommand>
    {
        public DeleteThemeCommandValidator()
        {
            RuleFor(command =>
                command.Id).NotEqual(Guid.Empty);
        }
    }
}
