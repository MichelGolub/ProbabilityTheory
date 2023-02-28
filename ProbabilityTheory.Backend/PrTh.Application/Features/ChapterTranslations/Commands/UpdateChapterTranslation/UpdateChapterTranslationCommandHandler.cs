using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Domain;
using PrTh.Application.Interfaces;
using PrTh.Application.Common.Exceptions;

namespace PrTh.Application.Features.ChapterTranslations.Commands.UpdateChapterTranslation
{
    public class UpdateChapterTranslationCommandHandler
        : IRequestHandler<UpdateChapterTranslationCommand>
    {
        private readonly IPrThDbContext _context;

        public UpdateChapterTranslationCommandHandler
            (IPrThDbContext context) => _context = context;

        public async Task<Unit> Handle(UpdateChapterTranslationCommand request,
            CancellationToken cancellationToken)
        {
            await checkLanguageAsync(request.LanguageId, cancellationToken);
            await checkChapterAsync(request.ChapterId, cancellationToken);

            var translation = await getChapterTranslationAsync
                (request.ChapterId, request.LanguageId, cancellationToken);

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

        private async Task<Unit> checkChapterAsync
            (Guid id, CancellationToken cancellationToken)
        {
            var _ = await _context.Chapters
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken)
                ?? throw new NotFoundException(nameof(Chapter), id);
            return Unit.Value;
        }

        private async Task<ChapterTranslation> getChapterTranslationAsync
            (Guid chapterId, Guid languageId, CancellationToken cancellationToken)
        {
            var translation = await _context.ChapterTranslations
                .FirstOrDefaultAsync(t => t.LanguageId == languageId
                && t.ChapterId == chapterId, cancellationToken);
            if (translation == null)
            {
                translation = new ChapterTranslation
                {
                    ChapterId = chapterId,
                    LanguageId = languageId
                };
                _context.ChapterTranslations.Add(translation);
            }

            return translation;
        }
    }
}
