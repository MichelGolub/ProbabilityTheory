using MediatR;

namespace PrTh.Application.Features.Terms.Queries.GetTermDetails
{
    public class GetTermDetailsQuery 
        : IRequest<TermDetailsVm>
    {
        public Guid LanguageId { get; set; }
        public Guid TermId { get; set; }
    }
}
