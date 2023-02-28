using FluentValidation;

namespace PrTh.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommandValidator
        : AbstractValidator<CreateQuestionCommand>
    {
        public CreateQuestionCommandValidator()
        {
            RuleFor(command =>
                command.ChapterId).NotEqual(Guid.Empty);
        }
    }
}
