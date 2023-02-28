using MediatR;

namespace PrTh.Application.Features.Steps.Commands.DeleteStep
{
    public class DeleteStepCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
