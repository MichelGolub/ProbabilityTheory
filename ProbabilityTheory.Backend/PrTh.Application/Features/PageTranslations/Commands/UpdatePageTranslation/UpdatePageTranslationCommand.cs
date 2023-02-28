using MediatR;

namespace PrTh.Application.Features.PageTranslations.Commands.UpdatePageTranslation
{
    public class UpdatePageTranslationCommand 
        : IRequest
    {
        public Guid PageId { get; set; }
        public Guid LanguageId { get; set; }
        public string Content { get; set; }
    }
}
