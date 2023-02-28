using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Domain;
using PrTh.Application.Interfaces;
using PrTh.Application.Common.Exceptions;

namespace PrTh.Application.Features.ThemeTranslations.Commands.UpdateThemeTranslation
{
    public class UpdateThemeTranslationCommandHandler
        : IRequestHandler<UpdateThemeTranslationCommand>
    {
        private readonly IPrThDbContext _context;

        public UpdateThemeTranslationCommandHandler
            (IPrThDbContext context) => _context = context;

        public async Task<Unit> Handle
            (UpdateThemeTranslationCommand request, CancellationToken cancellationToken)
        {
            await checkLanguageAsync(request.LanguageId, cancellationToken);
            await checkThemeAsync(request.ThemeId, cancellationToken);

            var translation = await getThemeTranslationAsync
                (request.ThemeId, request.LanguageId, cancellationToken);
            
            translation.TitleTranslation = request.Title;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task<Unit> checkLanguageAsync
            (Guid id, CancellationToken cancellationToken)
        {
            var _ = await _context.Languages
                .FirstOrDefaultAsync(l => l.Id == id, cancellationToken)
                ?? throw new NotFoundException(nameof(Language), id);
            return Unit.Value;
        }

        private async Task<Unit> checkThemeAsync
            (Guid id, CancellationToken cancellationToken)
        {
            var _ = await _context.Themes
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken)
                ?? throw new NotFoundException(nameof(Theme), id);
            return Unit.Value;
        }

        private async Task<ThemeTranslation> getThemeTranslationAsync
            (Guid themeId, Guid languageId, CancellationToken cancellationToken)
        {
            var translation = await _context.ThemeTranslations
                .FirstOrDefaultAsync(tt => tt.LanguageId == languageId
                && tt.ThemeId == themeId, cancellationToken);
            if(translation == null)
            {
                translation = new ThemeTranslation
                {
                    ThemeId = themeId,
                    LanguageId = languageId
                };
                _context.ThemeTranslations.Add(translation);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return translation;
        }
    }
}
