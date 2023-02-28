using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Application.Interfaces;
using PrTh.Application.Common.Exceptions;
using PrTh.Domain;

namespace PrTh.Application.Features.Pages.Queries.GetPageDetails
{
    public class GetPageDetailsQueryHandler
        : IRequestHandler<GetPageDetailsQuery, PageDetailsVm>
    {
        private readonly IPrThDbContext _context;
        private readonly IMapper _mapper;

        public GetPageDetailsQueryHandler(IPrThDbContext context,
            IMapper mapper) => (_context, _mapper) = (context, mapper);

        public async Task<PageDetailsVm> Handle(GetPageDetailsQuery request,
            CancellationToken cancellationToken)
        {
            await checkLanguageAsync(request.LanguageId, cancellationToken);

            var page = await getPageAsync(request.Id, cancellationToken);
            var translation = await getPageTranslationAsync
                (request.Id, request.LanguageId, cancellationToken);

            var pageVm = _mapper.Map<PageDetailsVm>(page);
            pageVm.Content = translation.ContentTranslation;

            return pageVm;
        }

        private async Task<Unit> checkLanguageAsync
            (Guid id, CancellationToken cancellationToken)
        {
            var _ = await _context.Languages
                .FirstOrDefaultAsync(l => l.Id == id, cancellationToken)
                ?? throw new NotFoundException(nameof(Language), id);
            return Unit.Value;
        }

        private async Task<Page> getPageAsync
            (Guid id, CancellationToken cancellationToken)
        {
            return await _context.Pages
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken)
                ?? throw new NotFoundException(nameof(Page), id);
        }

        private async Task<PageTranslation> getPageTranslationAsync
            (Guid pageId, Guid languageId, CancellationToken cancellationToken)
        {
            var translation = await _context.PageTranslations
                .FirstOrDefaultAsync(t => t.PageId == pageId
                    && t.LanguageId == languageId, cancellationToken);
            if (translation == null)
            {
                translation = new PageTranslation
                {
                    PageId = pageId,
                    LanguageId = languageId,
                    ContentTranslation = "-"
                };
                _context.PageTranslations.Add(translation);
            }
            return translation;
        }
    }
}
