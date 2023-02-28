using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Domain;
using PrTh.Application.Interfaces;
using PrTh.Application.Common.Exceptions;

namespace PrTh.Application.Features.PageTranslations.Commands.UpdatePageTranslation
{
    public class UpdatePageTranslationCommandHandler
        : IRequestHandler<UpdatePageTranslationCommand>
    {
        private readonly IPrThDbContext _context;

        public UpdatePageTranslationCommandHandler
            (IPrThDbContext context) => _context = context;

        public async Task<Unit> Handle(UpdatePageTranslationCommand request,
            CancellationToken cancellationToken)
        {
            await checkLanguageAsync(request.LanguageId, cancellationToken);
            await checkPageAsync(request.PageId, cancellationToken);

            var translation = await getPageTranslationAsync
                (request.PageId, request.LanguageId, cancellationToken);

            translation.ContentTranslation = request.Content;
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

        private async Task<Unit> checkPageAsync
            (Guid id, CancellationToken cancellationToken)
        {
            var _ = await _context.Pages
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
                ?? throw new NotFoundException(nameof(Page), id);
            return Unit.Value;
        }

        private async Task<PageTranslation> getPageTranslationAsync
            (Guid pageId, Guid languageId, CancellationToken cancellationToken)
        {
            var translation = await _context.PageTranslations
                .FirstOrDefaultAsync(t => t.LanguageId == languageId
                && t.PageId == pageId, cancellationToken);
            if(translation == null)
            {
                translation = new PageTranslation
                {
                    PageId = pageId,
                    LanguageId = languageId
                };
                _context.PageTranslations.Add(translation);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return translation;
        }
    }
}
