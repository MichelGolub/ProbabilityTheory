using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Application.Common.Exceptions;
using PrTh.Application.Interfaces;
using PrTh.Domain;

namespace PrTh.Application.Features.Terms.Queries.GetTermList
{
    public class GetTermListQueryHandler 
        : IRequestHandler<GetTermListQuery, TermListVm>
    {
        private readonly IPrThDbContext _context;
        private readonly IMapper _mapper;

        public GetTermListQueryHandler
            (IPrThDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<TermListVm> Handle
            (GetTermListQuery request, CancellationToken cancellationToken)
        {
            await checkLanguageAsync(request.LanguageId, cancellationToken);

            var termDtos = new List<TermLookupDto>();
            var terms = await getTermsAsync(cancellationToken);
            foreach (var term in terms)
            {
                var translation = await getTermTranslationAsync
                    (term.Id, request.LanguageId, cancellationToken);
                var termDto = _mapper.Map<TermLookupDto>(translation);
                termDtos.Add(termDto);
            }

            termDtos = termDtos.OrderBy(t => t.Title).ToList();

            return new TermListVm { Terms = termDtos };
        }

        private async Task<Unit> checkLanguageAsync
            (Guid languageId, CancellationToken cancellationToken)
        {
            var _ = await _context.Languages
                .FirstOrDefaultAsync(l => l.Id == languageId, cancellationToken)
                ?? throw new NotFoundException(nameof(Language), languageId);
            return Unit.Value;
        }

        private async Task<List<Term>> getTermsAsync
            (CancellationToken cancellationToken)
        {
            return await _context.Terms
                .ToListAsync(cancellationToken);
        }

        private async Task<TermTranslation> getTermTranslationAsync
            (Guid termId, Guid languageId, CancellationToken cancellationToken)
        {
            var translation = await _context.TermTranslations
                .FirstOrDefaultAsync(t => t.TermId == termId
                    && t.LanguageId == languageId, cancellationToken);
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
