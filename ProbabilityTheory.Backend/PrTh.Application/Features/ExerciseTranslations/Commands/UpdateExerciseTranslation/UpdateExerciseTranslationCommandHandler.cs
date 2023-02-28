using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Application.Common.Exceptions;
using PrTh.Application.Interfaces;
using PrTh.Domain;

namespace PrTh.Application.Features.ExerciseTranslations.Commands.UpdateExerciseTranslation
{
    public class UpdateExerciseTranslationCommandHandler
        : IRequestHandler<UpdateExerciseTranslationCommand>
    {
        private readonly IPrThDbContext _context;

        public UpdateExerciseTranslationCommandHandler
            (IPrThDbContext context) => _context = context;

        public async Task<Unit> Handle(UpdateExerciseTranslationCommand request,
            CancellationToken cancellationToken)
        {
            var _ = await _context.Languages
                .FirstOrDefaultAsync(l => l.Id == request.LanguageId, cancellationToken)
                ?? throw new NotFoundException(nameof(Language), request.LanguageId);
            var exercise = await _context.Exercises
                .FirstOrDefaultAsync(p => p.Id == request.ExerciseId, cancellationToken)
                ?? throw new NotFoundException(nameof(Exercise), request.ExerciseId);

            var translation = await _context.ExerciseTranslations
                .FirstOrDefaultAsync(t => t.LanguageId == request.LanguageId
                && t.ExerciseId == request.ExerciseId, cancellationToken);
            if (translation == null)
            {
                translation = new ExerciseTranslation
                {
                    ExerciseId = request.ExerciseId,
                    LanguageId = request.LanguageId
                };
                _context.ExerciseTranslations.Add(translation);
                await _context.SaveChangesAsync(cancellationToken);
            }
            translation.DescriptionTranslation = request.Description;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
