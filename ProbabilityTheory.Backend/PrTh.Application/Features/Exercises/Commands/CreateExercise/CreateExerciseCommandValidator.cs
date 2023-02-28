using FluentValidation;

namespace PrTh.Application.Features.Exercises.Commands.CreateExercise
{
    public class CreateExerciseCommandValidator
        : AbstractValidator<CreateExerciseCommand>
    {
        public CreateExerciseCommandValidator()
        {
            RuleFor(command =>
                command.ChapterId).NotEqual(Guid.Empty);
        }
    }
}
