using MediatR;

namespace PrTh.Application.Features.Terms.Commands.DeleteTerm
{
    public class DeleteTermCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
