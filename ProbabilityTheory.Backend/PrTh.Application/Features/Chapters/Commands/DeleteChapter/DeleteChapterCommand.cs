using MediatR;

namespace PrTh.Application.Features.Chapters.Commands.DeleteChapter
{
    public class DeleteChapterCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
