using MediatR;

namespace PrTh.Application.Features.Pages.Queries.GetPageDetails
{
    public class GetPageDetailsQuery 
        : IRequest<PageDetailsVm>
    {
        public Guid Id { get; set; }
        public Guid LanguageId { get; set; }
    }
}
