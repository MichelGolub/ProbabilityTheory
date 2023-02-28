using FluentValidation;

namespace PrTh.Application.Features.Exercises.Commands.DeleteExercise
{
    public class DeleteExerciseCommandValidator
        : AbstractValidator<DeleteExerciseCommand>
    {
        public DeleteExerciseCommandValidator()
        {
            RuleFor(command =>
                command.Id).NotEqual(Guid.Empty);
        }
    }
}
