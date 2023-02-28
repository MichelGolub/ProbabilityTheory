using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Domain;
using PrTh.Application.Interfaces;

namespace PrTh.Application.Features.Themes.Commands.CreateTheme
{
    public class CreateThemeCommandHandler : IRequestHandler<CreateThemeCommand, Guid>
    {
        private readonly IPrThDbContext _dpContext;
        
        public CreateThemeCommandHandler
            (IPrThDbContext dbContext) => _dpContext = dbContext;

        public async Task<Guid> Handle
            (CreateThemeCommand request, CancellationToken cancellationToken)
        {
            var themes = await _dpContext.Themes.ToListAsync(cancellationToken);
            var theme = new Theme
            {
                Id = Guid.NewGuid(),
                Number = themes.Count + 1
            };

            await _dpContext.Themes.AddAsync(theme, cancellationToken);
            await _dpContext.SaveChangesAsync(cancellationToken);

            return theme.Id;
        }
    }
}
