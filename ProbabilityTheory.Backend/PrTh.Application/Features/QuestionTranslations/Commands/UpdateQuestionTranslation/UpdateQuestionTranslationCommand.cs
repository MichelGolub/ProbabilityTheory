using MediatR;

namespace PrTh.Application.Features.QuestionTranslations.Commands.UpdateQuestionTranslation
{
    public class UpdateQuestionTranslationCommand
        : IRequest
    {
        public Guid QuestionId { get; set; }
        public Guid LanguageId { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; }
    }
}
