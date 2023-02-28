using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Domain;
using PrTh.Application.Interfaces;
using PrTh.Application.Common.Exceptions;

namespace PrTh.Application.Features.Chapters.Commands.DeleteChapter
{
    public class DeleteChapterCommandHandler
        : IRequestHandler<DeleteChapterCommand>
    {
        private readonly IPrThDbContext _context;

        public DeleteChapterCommandHandler(IPrThDbContext context) =>
            _context = context;

        public async Task<Unit> Handle(DeleteChapterCommand request,
            CancellationToken cancellationToken)
        {
            var chapter = await getChapterAsync(request.Id, cancellationToken);
            var chapters = await getChaptersGreaterThanChapterAsync
                (chapter, cancellationToken);
            foreach (var c in chapters)
            {
                c.Number--;
            }

            _context.Chapters.Remove(chapter);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task<Chapter> getChapterAsync
            (Guid id, CancellationToken cancellationToken)
        {
            return await _context.Chapters
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken)
                ?? throw new NotFoundException(nameof(Chapter), id);
        }

        private async Task<List<Chapter>> getChaptersGreaterThanChapterAsync
            (Chapter chapter, CancellationToken cancellationToken)
        {
            return await _context.Chapters
                .Where(c => c.ThemeId == chapter.ThemeId 
                    && c.Number > chapter.Number)
                .ToListAsync(cancellationToken);
        }
    }
}
