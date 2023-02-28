using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Application.Common.Exceptions;
using PrTh.Application.Interfaces;
using PrTh.Domain;

namespace PrTh.Application.Features.StepTranslations.Commands.UpdateStepTranslation
{
    public class UpdateStepTranslationCommandHandler
        : IRequestHandler<UpdateStepTranslationCommand>
    {
        private readonly IPrThDbContext _context;

        public UpdateStepTranslationCommandHandler(IPrThDbContext context)
            => _context = context;

        public async Task<Unit> Handle(UpdateStepTranslationCommand request,
            CancellationToken cancellationToken)
        {
            var _ = await _context.Languages
                .FirstOrDefaultAsync(l => l.Id == request.LanguageId, cancellationToken)
                ?? throw new NotFoundException(nameof(Language), request.LanguageId);
            var step = await _context.Steps
                .FirstOrDefaultAsync(p => p.Id == request.StepId, cancellationToken)
                ?? throw new NotFoundException(nameof(Step), request.StepId);

            var translation = await _context.StepTranslations
                .FirstOrDefaultAsync(t => t.LanguageId == request.LanguageId
                && t.StepId == request.StepId, cancellationToken);
            if (translation == null)
            {
                translation = new StepTranslation
                {
                    StepId = request.StepId,
                    LanguageId = request.LanguageId
                };

                _context.StepTranslations.Add(translation);
                await _context.SaveChangesAsync(cancellationToken);
            }

            translation.DescriptionTranslation = request.Description;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
