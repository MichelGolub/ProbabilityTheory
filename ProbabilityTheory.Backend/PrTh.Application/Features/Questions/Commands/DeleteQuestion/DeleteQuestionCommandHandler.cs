using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Domain;
using PrTh.Application.Interfaces;
using PrTh.Application.Common.Exceptions;

namespace PrTh.Application.Features.Questions.Commands.DeleteQuestion
{
    public class DeleteQuestionCommandHandler
        : IRequestHandler<DeleteQuestionCommand>
    {
        private readonly IPrThDbContext _context;

        public DeleteQuestionCommandHandler(IPrThDbContext context) =>
            _context = context;

        public async Task<Unit> Handle(DeleteQuestionCommand request,
            CancellationToken cancellationToken)
        {
            var question = await getQuestionAsync(request.Id, cancellationToken);

            var questions = await _context.Questions
                .Where(q => q.ChapterId == question.ChapterId 
                    && q.Number > question.Number)
                .ToListAsync(cancellationToken);
            foreach (var q in questions)
            {
                q.Number--;
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task<Question> getQuestionAsync
            (Guid id, CancellationToken cancellationToken)
        {
            return await _context.Questions
                .FirstOrDefaultAsync(q => q.Id == id, cancellationToken)
                ?? throw new NotFoundException(nameof(Question), id);
        }
    }
}
