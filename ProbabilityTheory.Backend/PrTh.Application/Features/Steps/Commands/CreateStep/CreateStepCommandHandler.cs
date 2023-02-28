

using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Application.Common.Exceptions;
using PrTh.Application.Interfaces;
using PrTh.Domain;

namespace PrTh.Application.Features.Steps.Commands.CreateStep
{
    public class CreateStepCommandHandler
        : IRequestHandler<CreateStepCommand, Guid>
    {
        private readonly IPrThDbContext _context;

        public CreateStepCommandHandler(IPrThDbContext context) =>
            _context = context;

        public async Task<Guid> Handle(CreateStepCommand request,
            CancellationToken cancellationToken)
        {
            var _ = _context.Exercises
                .FirstOrDefaultAsync(c => c.Id == request.ExerciseId,
                    cancellationToken)
                ?? throw new NotFoundException(nameof(Exercise), request.ExerciseId);

            var steps = await _context.Steps
                .Where(s => s.ExerciseId == request.ExerciseId)
                .ToListAsync(cancellationToken);

            var step = new Step
            {
                Id = Guid.NewGuid(),
                ExerciseId = request.ExerciseId,
                Number = steps.Count + 1
            };

            _context.Steps.Add(step);
            await _context.SaveChangesAsync(cancellationToken);

            return step.Id;
        }
    }
}
