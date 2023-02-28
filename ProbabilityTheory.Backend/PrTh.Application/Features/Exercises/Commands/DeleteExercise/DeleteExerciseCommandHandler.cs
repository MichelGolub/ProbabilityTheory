using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Application.Common.Exceptions;
using PrTh.Application.Interfaces;
using PrTh.Domain;

namespace PrTh.Application.Features.Exercises.Commands.DeleteExercise
{
    public class DeleteExerciseCommandHandler
        : IRequestHandler<DeleteExerciseCommand>
    {
        private readonly IPrThDbContext _context;

        public DeleteExerciseCommandHandler
            (IPrThDbContext context) => _context = context;

        public async Task<Unit> Handle(DeleteExerciseCommand request,
            CancellationToken cancellationToken)
        {
            var exercise = await _context.Exercises
                    .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                    ?? throw new NotFoundException(nameof(Exercise), request.Id);

            var exercises = await _context.Exercises
                .Where(e => e.ChapterId == exercise.ChapterId
                    && e.Number > exercise.Number)
                .ToListAsync(cancellationToken);
            foreach (var e in exercises)
            {
                e.Number--;
            }

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
