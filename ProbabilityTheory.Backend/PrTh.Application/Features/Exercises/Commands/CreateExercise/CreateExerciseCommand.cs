using MediatR;

namespace PrTh.Application.Features.Exercises.Commands.CreateExercise
{
    public class CreateExerciseCommand 
        : IRequest<Guid>
    {
        public Guid ChapterId { get; set; }
    }
}
