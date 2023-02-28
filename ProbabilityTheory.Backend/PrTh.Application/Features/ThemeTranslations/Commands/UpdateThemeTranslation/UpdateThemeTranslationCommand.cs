using MediatR;

namespace PrTh.Application.Features.ThemeTranslations.Commands.UpdateThemeTranslation
{
    public class UpdateThemeTranslationCommand 
        : IRequest
    {
        public Guid LanguageId { get; set; }
        public Guid ThemeId { get; set; }
        public string Title { get; set; }
    }
}
