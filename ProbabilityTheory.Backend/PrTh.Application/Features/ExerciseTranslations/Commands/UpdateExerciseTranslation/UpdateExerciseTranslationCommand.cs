using MediatR;

namespace PrTh.Application.Features.ExerciseTranslations.Commands.UpdateExerciseTranslation
{
    public class UpdateExerciseTranslationCommand
        : IRequest
    {
        public Guid ExerciseId { get; set; }
        public Guid LanguageId { get; set; }
        public string Description { get; set; }
    }
}
