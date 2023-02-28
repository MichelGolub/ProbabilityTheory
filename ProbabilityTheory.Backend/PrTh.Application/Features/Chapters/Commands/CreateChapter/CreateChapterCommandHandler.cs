using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Application.Common.Exceptions;
using PrTh.Application.Interfaces;
using PrTh.Domain;

namespace PrTh.Application.Features.Chapters.Commands.CreateChapter
{
    public class CreateChapterCommandHandler
        : IRequestHandler<CreateChapterCommand, Guid>
    {
        private readonly IPrThDbContext _context;

        public CreateChapterCommandHandler(IPrThDbContext context) =>
            _context = context;

        public async Task<Guid> Handle(CreateChapterCommand request,
            CancellationToken cancellationToken)
        {
            await checkThemeAsync(request.ThemeId, cancellationToken);

            var chapters = await getChaptersByThemeIdAsync
                (request.ThemeId, cancellationToken);

            var chapter = new Chapter
            {
                Id = Guid.NewGuid(),
                ThemeId = request.ThemeId,
                Number = chapters.Count + 1
            };

            _context.Chapters.Add(chapter);
            await _context.SaveChangesAsync(cancellationToken);

            return chapter.Id;
        }

        private async Task<Unit> checkThemeAsync(Guid id, CancellationToken cancellationToken)
        {
            var _ = await _context.Themes
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken)
                ?? throw new NotFoundException(nameof(Theme), id);
            return Unit.Value;
        }

        private async Task<List<Chapter>> getChaptersByThemeIdAsync
            (Guid themeId, CancellationToken cancellationToken)
        {
            return await _context.Chapters
                .Where(c => c.ThemeId == themeId)
                .ToListAsync(cancellationToken);
        }
    }
}
