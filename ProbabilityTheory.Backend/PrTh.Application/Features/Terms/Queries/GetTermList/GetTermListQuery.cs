using MediatR;

namespace PrTh.Application.Features.Terms.Queries.GetTermList
{
    public class GetTermListQuery 
        : IRequest<TermListVm>
    {
        public Guid LanguageId { get; set; }
    }
}
