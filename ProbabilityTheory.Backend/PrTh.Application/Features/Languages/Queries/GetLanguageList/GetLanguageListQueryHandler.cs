using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PrTh.Application.Interfaces;

namespace PrTh.Application.Features.Languages.Queries.GetLanguageList
{
    public class GetLanguageListQueryHandler 
        : IRequestHandler<GetLanguageListQuery, LanguageListVm>
    {
        private readonly IPrThDbContext _context;
        private readonly IMapper _mapper;

        public GetLanguageListQueryHandler(IPrThDbContext context,
            IMapper mapper) => (_context, _mapper) = (context, mapper);

        public async Task<LanguageListVm> Handle(GetLanguageListQuery request,
            CancellationToken cancellationToken)
        {
            var languages = await _context.Languages
                .ProjectTo<LanguageLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new LanguageListVm { Languages = languages };
        }
    }
}
