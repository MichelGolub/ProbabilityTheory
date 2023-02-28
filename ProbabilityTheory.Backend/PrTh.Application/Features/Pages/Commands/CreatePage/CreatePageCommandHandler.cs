using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Application.Common.Exceptions;
using PrTh.Application.Interfaces;
using PrTh.Domain;

namespace PrTh.Application.Features.Pages.Commands.CreatePage
{
    public class CreatePageCommandHandler
        : IRequestHandler<CreatePageCommand, Guid>
    {
        private readonly IPrThDbContext _context;

        public CreatePageCommandHandler(IPrThDbContext context) =>
            _context = context;

        public async Task<Guid> Handle(CreatePageCommand request,
            CancellationToken cancellationToken)
        {
            await checkChapterAsync(request.ChapterId, cancellationToken);

            var pages = await _context.Pages
                .Where(p => p.ChapterId == request.ChapterId)
                .ToListAsync(cancellationToken);
            var page = new Page
            {
                Id = Guid.NewGuid(),
                ChapterId = request.ChapterId,
                Number = pages.Count + 1
            };

            _context.Pages.Add(page);
            await _context.SaveChangesAsync(cancellationToken);

            return page.Id;
        }

        private async Task<Unit> checkChapterAsync
            (Guid id, CancellationToken cancellationToken)
        {
            var _ = _context.Chapters
                .FirstOrDefaultAsync(c => c.Id == id,
                    cancellationToken)
                ?? throw new NotFoundException(nameof(Chapter), id);
            return Unit.Value;
        }
    }
}
