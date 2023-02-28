using MediatR;

namespace PrTh.Application.Features.Chapters.Commands.CreateChapter
{
    public class CreateChapterCommand 
        : IRequest<Guid>
    {
        public Guid ThemeId { get; set; }
    }
}
