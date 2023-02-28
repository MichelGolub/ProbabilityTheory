using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Application.Common.Exceptions;
using PrTh.Application.Interfaces;
using PrTh.Domain;

namespace PrTh.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommandHandler
        : IRequestHandler<CreateQuestionCommand, Guid>
    {
        private readonly IPrThDbContext _context;

        public CreateQuestionCommandHandler(IPrThDbContext context) =>
            _context = context;

        public async Task<Guid> Handle(CreateQuestionCommand request,
            CancellationToken cancellationToken)
        {
            await checkChapterAsync(request.ChapterId, cancellationToken);

            var questions = await _context.Questions
                .Where(p => p.ChapterId == request.ChapterId)
                .ToListAsync(cancellationToken);
            var question = new Question
            {
                Id = Guid.NewGuid(),
                ChapterId = request.ChapterId,
                Number = questions.Count + 1
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync(cancellationToken);

            return question.Id;
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
