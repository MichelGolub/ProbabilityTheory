using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Application.Common.Exceptions;
using PrTh.Application.Interfaces;
using PrTh.Domain;

namespace PrTh.Application.Features.Terms.Queries.GetTermDetails
{
    public class GetTermDetailsQueryHandler
        : IRequestHandler<GetTermDetailsQuery, TermDetailsVm>
    {
        private readonly IPrThDbContext _context;
        private readonly IMapper _mapper;

        public GetTermDetailsQueryHandler
            (IPrThDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<TermDetailsVm> Handle(GetTermDetailsQuery request, CancellationToken cancellationToken)
        {
            await checkLanguageAsync(request.LanguageId, cancellationToken);
            await checkTermAsync(request.TermId, cancellationToken);

            var translation = await getTermTranslationAsync(request.TermId,
                request.LanguageId, cancellationToken);
            return _mapper.Map<TermDetailsVm>(translation);
        }

        private async Task<Unit> checkLanguageAsync
            (Guid languageId, CancellationToken cancellationToken)
        {
            var _ = await _context.Languages
                .FirstOrDefaultAsync(l => l.Id == languageId,
                    cancellationToken)
                ?? throw new NotFoundException(nameof(Language), languageId);
            return Unit.Value;
        }

        private async Task<Unit> checkTermAsync(Guid termId, CancellationToken cancellationToken)
        {
            var _ = await _context.Terms
                .FirstOrDefaultAsync(t => t.Id == termId,
                    cancellationToken)
                ?? throw new NotFoundException(nameof(Term), termId);
            return Unit.Value;
        }

        private async Task<TermTranslation> getTermTranslationAsync
            (Guid termId, Guid languageId, CancellationToken cancellationToken)
        {
            var translation = await _context.TermTranslations
                .FirstOrDefaultAsync(t => t.LanguageId == languageId
                    && t.TermId == termId, cancellationToken);
            if (translation == null)
            {
                translation = new TermTranslation
                {
                    TermId = termId,
                    LanguageId = languageId,
                    TitleTranslation = "-",
                    DefinitionTranslation = "-"
                };
                _context.TermTranslations.Add(translation);
            }
            return translation;
        }
    }
}
