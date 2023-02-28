using MediatR;

namespace PrTh.Application.Features.Steps.Commands.CreateStep
{
    public class CreateStepCommand 
        : IRequest<Guid>
    {
        public Guid ExerciseId { get; set; }
    }
}
