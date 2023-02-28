using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Application.Common.Exceptions;
using PrTh.Application.Interfaces;
using PrTh.Domain;

namespace PrTh.Application.Features.Terms.Commands.DeleteTerm
{
    public class DeleteTermCommandHandler : IRequestHandler<DeleteTermCommand>
    {
        private readonly IPrThDbContext _context;

        public DeleteTermCommandHandler
            (IPrThDbContext context) => _context = context;

        public async Task<Unit> Handle
            (DeleteTermCommand request, CancellationToken cancellationToken)
        {
            var term = await _context.Terms
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Term), request.Id);

            _context.Terms.Remove(term);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
