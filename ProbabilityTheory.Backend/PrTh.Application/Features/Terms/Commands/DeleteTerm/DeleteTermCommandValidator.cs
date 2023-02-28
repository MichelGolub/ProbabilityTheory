using FluentValidation;

namespace PrTh.Application.Features.Terms.Commands.DeleteTerm
{
    public class DeleteTermCommandValidator 
        : AbstractValidator<DeleteTermCommand>
    {
        public DeleteTermCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
