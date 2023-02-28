using FluentValidation;

namespace PrTh.Application.Features.Questions.Commands.DeleteQuestion
{
    public class DeleteQuestionCommandValidator
        : AbstractValidator<DeleteQuestionCommand>
    {
        public DeleteQuestionCommandValidator()
        {
            RuleFor(c => c.Id).NotEqual(Guid.Empty);
        }
    }
}
