using MediatR;
using PrTh.Application.Interfaces;
using PrTh.Domain;

namespace PrTh.Application.Features.Terms.Commands.CreateTerm
{
    public class CreateTermCommandHandler 
        : IRequestHandler<CreateTermCommand, Guid>
    {
        private readonly IPrThDbContext _context;

        public CreateTermCommandHandler
            (IPrThDbContext context) => _context = context;

        public async Task<Guid> Handle(CreateTermCommand request,
            CancellationToken cancellationToken)
        {
            var term = new Term
            {
                Id = Guid.NewGuid()
            };

            _context.Terms.Add(term);
            await _context.SaveChangesAsync(cancellationToken);

            return term.Id;
        }
    }
}
