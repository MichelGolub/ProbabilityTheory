using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Application.Common.Exceptions;
using PrTh.Application.Interfaces;
using PrTh.Domain;

namespace PrTh.Application.Features.TermTranslations.Commands.UpdateTermTranslation
{
    public class UpdateTermTranslationCommandHandler
        : IRequestHandler<UpdateTermTranslationCommand>
    {
        private readonly IPrThDbContext _context;

        public UpdateTermTranslationCommandHandler
            (IPrThDbContext context) => _context = context;

        public async Task<Unit> Handle
            (UpdateTermTranslationCommand request, CancellationToken cancellationToken)
        {
            await checkLanguageAsync(request.LanguageId, cancellationToken);
            await checkTermAsync(request.TermId, cancellationToken);

            var translation = await getTermTranslationAsync(request.TermId,
                request.LanguageId, cancellationToken);

            translation.TitleTranslation = request.Title;
            translation.DefinitionTranslation = request.Definition;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        private async Task<Unit> checkTermAsync
            (Guid termId, CancellationToken cancellationToken)
        {
            var _ = await _context.Terms
                .FirstOrDefaultAsync(t => t.Id == termId, cancellationToken)
                ?? throw new NotFoundException(nameof(Term), termId);
            return Unit.Value;
        }

        private async Task<Unit> checkLanguageAsync
            (Guid languageId, CancellationToken cancellationToken)
        {
            var _ = await _context.Languages
                .FirstOrDefaultAsync(l => l.Id == languageId, cancellationToken)
                ?? throw new NotFoundException(nameof(Language), languageId);
            return Unit.Value;
        }

        private async Task<TermTranslation> getTermTranslationAsync
            (Guid termId, Guid languageId, CancellationToken cancellationToken)
        {
            var translation = await _context.TermTranslations
                .FirstOrDefaultAsync(tt => tt.TermId == termId
                    && tt.LanguageId == languageId, cancellationToken);
            if (translation == null)
            {
                translation = new TermTranslation
                {
                    TermId = termId,
                    LanguageId = languageId,
                };
                _context.TermTranslations.Add(translation);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return translation;
        }
    }
}
