using FluentValidation;

namespace PrTh.Application.Features.Pages.Commands.CreatePage
{
    public class CreatePageCommandValidator 
        : AbstractValidator<CreatePageCommand>
    {
        public CreatePageCommandValidator()
        {
            RuleFor(command =>
                command.ChapterId).NotEqual(Guid.Empty);
        }
    }
}
