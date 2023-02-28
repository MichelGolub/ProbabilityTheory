using MediatR;

namespace PrTh.Application.Features.TermTranslations.Commands.UpdateTermTranslation
{
    public class UpdateTermTranslationCommand 
        : IRequest
    {
        public Guid TermId { get; set; }
        public Guid LanguageId { get; set; }
        public string Title { get; set; }
        public string Definition { get; set; }
    }
}
