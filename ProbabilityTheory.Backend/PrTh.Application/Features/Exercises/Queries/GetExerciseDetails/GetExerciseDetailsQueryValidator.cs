using FluentValidation;

namespace PrTh.Application.Features.Exercises.Queries.GetExerciseDetails
{
    public class GetExerciseDetailsQueryValidator
        : AbstractValidator<GetExerciseDetailsQuery>
    {
        public GetExerciseDetailsQueryValidator()
        {
            RuleFor(q => q.Id)
                .NotEqual(Guid.Empty);
            RuleFor(q => q.LanguageId)
                .NotEqual(Guid.Empty);
        }
    }
}
