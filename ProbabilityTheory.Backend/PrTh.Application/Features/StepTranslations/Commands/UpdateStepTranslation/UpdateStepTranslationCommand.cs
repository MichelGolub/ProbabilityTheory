using MediatR;

namespace PrTh.Application.Features.StepTranslations.Commands.UpdateStepTranslation
{
    public class UpdateStepTranslationCommand
        : IRequest
    {
        public Guid StepId { get; set; }
        public Guid LanguageId { get; set; }
        public string Description { get; set; }
    }
}
