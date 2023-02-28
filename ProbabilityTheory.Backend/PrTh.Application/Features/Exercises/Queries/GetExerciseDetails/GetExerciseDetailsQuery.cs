using MediatR;

namespace PrTh.Application.Features.Exercises.Queries.GetExerciseDetails
{
    public class GetExerciseDetailsQuery 
        : IRequest<ExerciseDetailsVm>
    {
        public Guid Id { get; set; }
        public Guid LanguageId { get; set; }
    }
}
