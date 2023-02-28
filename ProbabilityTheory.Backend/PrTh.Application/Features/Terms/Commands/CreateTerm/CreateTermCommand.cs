using MediatR;

namespace PrTh.Application.Features.Terms.Commands.CreateTerm
{
    public class CreateTermCommand 
        : IRequest<Guid> { }
}
