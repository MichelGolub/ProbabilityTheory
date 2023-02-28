using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Application.Common.Exceptions;
using PrTh.Application.Interfaces;
using PrTh.Domain;

namespace PrTh.Application.Features.Steps.Commands.DeleteStep
{
    public class DeleteStepCommandValidator
        : AbstractValidator<DeleteStepCommand>
    {
        public class DeleteStepCommandHandler
        : IRequestHandler<DeleteStepCommand>
        {
            private readonly IPrThDbContext _context;

            public DeleteStepCommandHandler(IPrThDbContext context) =>
                _context = context;

            public async Task<Unit> Handle(DeleteStepCommand request,
                CancellationToken cancellationToken)
            {
                var step = await _context.Steps
                    .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
                    ?? throw new NotFoundException(nameof(Step), request.Id);

                var steps = await _context.Steps
                    .Where(s => s.ExerciseId == step.ExerciseId
                        && s.Number > s.Number)
                    .ToListAsync(cancellationToken);
                foreach (var s in steps)
                {
                    s.Number--;
                }

                _context.Steps.Remove(step);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
