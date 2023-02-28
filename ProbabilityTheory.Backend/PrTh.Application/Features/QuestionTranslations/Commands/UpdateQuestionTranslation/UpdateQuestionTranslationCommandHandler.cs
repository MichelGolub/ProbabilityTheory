using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Domain;
using PrTh.Application.Interfaces;
using PrTh.Application.Common.Exceptions;

namespace PrTh.Application.Features.QuestionTranslations.Commands.UpdateQuestionTranslation
{
    public class UpdateQuestionTranslationCommandHandler
        : IRequestHandler<UpdateQuestionTranslationCommand>
    {
        private readonly IPrThDbContext _context;

        public UpdateQuestionTranslationCommandHandler
            (IPrThDbContext context) => _context = context;

        public async Task<Unit> Handle
            (UpdateQuestionTranslationCommand request, CancellationToken cancellationToken)
        {
            await checkLanguageAsync(request.LanguageId, cancellationToken);
            await checkQuestionAsync(request.QuestionId, cancellationToken);

            var translation = await getQuestionTranslationAsync
                (request.QuestionId, request.LanguageId, cancellationToken);

            translation.DescriptionTranslation = request.Description;
            translation.AnswerTranslation = request.Answer;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task<Unit> checkLanguageAsync
            (Guid id, CancellationToken cancellationToken)
        {
            var _ = await _context.Languages
                .FirstOrDefaultAsync(l => l.Id == id, cancellationToken)
                ?? throw new NotFoundException(nameof(Language), id);
            return Unit.Value;
        }

        private async Task<Unit> checkQuestionAsync
            (Guid id, CancellationToken cancellationToken)
        {
            var _ = await _context.Questions
                .FirstOrDefaultAsync(q => q.Id == id, cancellationToken)
                ?? throw new NotFoundException(nameof(Question), id);
            return Unit.Value;
        }

        private async Task<QuestionTranslation> getQuestionTranslationAsync
            (Guid questionId, Guid languageId, CancellationToken cancellationToken)
        {
            var translation = await _context.QuestionTranslations
                .FirstOrDefaultAsync(t => t.LanguageId == languageId
                && t.QuestionId == questionId, cancellationToken);
            if (translation == null)
            {
                translation = new QuestionTranslation
                {
                    QuestionId = questionId,
                    LanguageId = languageId
                };
                _context.QuestionTranslations.Add(translation);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return translation;
        }
    }
}
