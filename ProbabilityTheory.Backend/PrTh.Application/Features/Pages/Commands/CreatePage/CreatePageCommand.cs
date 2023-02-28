using MediatR;

namespace PrTh.Application.Features.Pages.Commands.CreatePage
{
    public class CreatePageCommand 
        : IRequest<Guid>
    {
        public Guid ChapterId { get; set; }
    }
}
