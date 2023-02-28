using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Application.Common.Exceptions;
using PrTh.Application.Interfaces;
using PrTh.Domain;

namespace PrTh.Application.Features.Exercises.Commands.CreateExercise
{
    public class CreateExerciseCommandHandler
        : IRequestHandler<CreateExerciseCommand, Guid>
    {
        private readonly IPrThDbContext _context;

        public CreateExerciseCommandHandler(IPrThDbContext context) =>
            _context = context;

        public async Task<Guid> Handle
            (CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            await checkChapterAsync(request.ChapterId, cancellationToken);

            var exercises = await _context.Exercises
                .Where(e => e.ChapterId == request.ChapterId)
                .ToListAsync(cancellationToken);
            var exercise = new Exercise
            {
                Id = Guid.NewGuid(),
                ChapterId = request.ChapterId,
                Number = exercises.Count + 1
            };

            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync(cancellationToken);

            return exercise.Id;
        }

        private async Task<Unit> checkChapterAsync
            (Guid id, CancellationToken cancellationToken)
        {
            var _ = await _context.Chapters
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken)
                ?? throw new NotFoundException(nameof(Chapter), id);
            return Unit.Value;
        }
    }
}
