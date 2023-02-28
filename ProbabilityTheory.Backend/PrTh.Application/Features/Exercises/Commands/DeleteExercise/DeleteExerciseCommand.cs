using MediatR;

namespace PrTh.Application.Features.Exercises.Commands.DeleteExercise
{
    public class DeleteExerciseCommand 
        : IRequest
    {
        public Guid Id { get; set; }
    }
}
