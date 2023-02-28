using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Application.Interfaces;
using PrTh.Application.Common.Exceptions;
using PrTh.Domain;

namespace PrTh.Application.Features.Questions.Queries.GetQuestionDetails
{
    public class GetQuestionDetailsQueryHandler
        : IRequestHandler<GetQuestionDetailsQuery, QuestionDetailsVm>
    {
        private readonly IPrThDbContext _context;
        private readonly IMapper _mapper;

        public GetQuestionDetailsQueryHandler(IPrThDbContext context,
            IMapper mapper) => (_context, _mapper) = (context, mapper);

        public async Task<QuestionDetailsVm> Handle
            (GetQuestionDetailsQuery request, CancellationToken cancellationToken)
        {
            await checkLanguageAsync(request.LanguageId, cancellationToken);

            var question = await getQuestionAsync(request.Id, cancellationToken);
            var translation = await getQuestionTranslationAsync
                (request.Id, request.LanguageId, cancellationToken);

            var questionVm = _mapper.Map<QuestionDetailsVm>(translation);
            questionVm.ChapterId = question.ChapterId;

            return questionVm;
        }

        private async Task<Unit> checkLanguageAsync
            (Guid id, CancellationToken cancellationToken)
        {
            var _ = await _context.Languages
                .FirstOrDefaultAsync(l => l.Id == id, cancellationToken)
                ?? throw new NotFoundException(nameof(Language), id);
            return Unit.Value;
        }

        private async Task<Question> getQuestionAsync
            (Guid id, CancellationToken cancellationToken)
        {
            return await _context.Questions
                .FirstOrDefaultAsync(q => q.Id == id, cancellationToken)
                ?? throw new NotFoundException(nameof(Question), id);
        }

        private async Task<QuestionTranslation> getQuestionTranslationAsync
            (Guid questionId, Guid languageId, CancellationToken cancellationToken)
        {
            var translation = await _context.QuestionTranslations
                .FirstOrDefaultAsync(t => t.QuestionId == questionId
                    && t.LanguageId == languageId, cancellationToken);
            if (translation == null)
            {
                translation = new QuestionTranslation
                {
                    QuestionId = questionId,
                    LanguageId = languageId,
                    DescriptionTranslation = "-",
                    AnswerTranslation = "-"
                };
                _context.QuestionTranslations.Add(translation);
            }
            return translation;
        }
    }
}
