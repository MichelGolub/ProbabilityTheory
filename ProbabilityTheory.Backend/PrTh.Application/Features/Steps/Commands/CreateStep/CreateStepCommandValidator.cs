using FluentValidation;

namespace PrTh.Application.Features.Steps.Commands.CreateStep
{
    public class CreateStepCommandValidator
        : AbstractValidator<CreateStepCommand>
    {
        public CreateStepCommandValidator()
        {
            RuleFor(command =>
                command.ExerciseId).NotEqual(Guid.Empty);
        }
    }
}
