using FluentValidation;

namespace PrTh.Application.Features.Pages.Commands.DeletePage
{
    public class DeletePageCommandValidator 
        : AbstractValidator<DeletePageCommand>
    {
        public DeletePageCommandValidator()
        {
            RuleFor(c => c.Id).NotEqual(Guid.Empty);
        }
    }
}
