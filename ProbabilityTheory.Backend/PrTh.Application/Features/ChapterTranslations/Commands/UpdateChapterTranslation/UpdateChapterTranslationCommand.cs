using MediatR;

namespace PrTh.Application.Features.ChapterTranslations.Commands.UpdateChapterTranslation
{
    public class UpdateChapterTranslationCommand 
        : IRequest
    {
        public Guid ChapterId { get; set; }
        public Guid LanguageId { get; set; }
        public string Title { get; set; }
    }
}
