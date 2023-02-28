using MediatR;

namespace PrTh.Application.Features.Chapters.Queries.GetChapterDetails
{
    public class GetChapterDetailsQuery 
        : IRequest<ChapterDetailsVm>
    {
        public Guid Id { get; set; }
        public Guid LanguageId { get; set; }
    }
}
