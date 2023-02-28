using MediatR;

namespace PrTh.Application.Features.Pages.Commands.DeletePage
{
    public class DeletePageCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
