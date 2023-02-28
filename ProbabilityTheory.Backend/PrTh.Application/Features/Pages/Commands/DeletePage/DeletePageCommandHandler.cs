using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Domain;
using PrTh.Application.Interfaces;
using PrTh.Application.Common.Exceptions;

namespace PrTh.Application.Features.Pages.Commands.DeletePage
{
    public class DeletePageCommandHandler
        : IRequestHandler<DeletePageCommand>
    {
        private readonly IPrThDbContext _context;

        public DeletePageCommandHandler(IPrThDbContext context) =>
            _context = context;

        public async Task<Unit> Handle(DeletePageCommand request,
            CancellationToken cancellationToken)
        {
            var page = await GetPageAsync(request.Id, cancellationToken);

            var pages = await _context.Pages
                .Where(p => p.ChapterId == page.ChapterId 
                    && p.Number > page.Number)
                .ToListAsync(cancellationToken);
            foreach (var p in pages)
            {
                p.Number--;
            }

            _context.Pages.Remove(page);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task<Page> GetPageAsync
            (Guid id, CancellationToken cancellationToken)
        {
            return await _context.Pages
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
                ?? throw new NotFoundException(nameof(Page), id);
        }
    }
}
