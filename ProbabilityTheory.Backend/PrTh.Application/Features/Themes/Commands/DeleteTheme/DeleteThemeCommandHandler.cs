using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Domain;
using PrTh.Application.Interfaces;
using PrTh.Application.Common.Exceptions;

namespace PrTh.Application.Features.Themes.Commands.DeleteTheme
{
    public class DeleteThemeCommandHandler
        : IRequestHandler<DeleteThemeCommand>
    {
        private readonly IPrThDbContext _context;

        public DeleteThemeCommandHandler
            (IPrThDbContext context) => _context = context;
        
        public async Task<Unit> Handle(DeleteThemeCommand request,
            CancellationToken cancellationToken)
        {
            var theme = await GetThemeAsync(request.Id, cancellationToken);
            var themes = await GetThemesGreaterThanThemeAsync
                (theme, cancellationToken);
            foreach (var t in themes)
            {
                t.Number--;
            }

            _context.Themes.Remove(theme);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        public async Task<Theme> GetThemeAsync
            (Guid id, CancellationToken cancellationToken)
        {
            return await _context.Themes
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken)
                ?? throw new NotFoundException(nameof(Theme), id);
        }

        public async Task<List<Theme>> GetThemesGreaterThanThemeAsync
            (Theme theme, CancellationToken cancellationToken)
        {
            return await _context.Themes
                .Where(t => t.Number > theme.Number)
                .ToListAsync(cancellationToken);
        }
    }
}
