using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrTh.Application.Interfaces;
using PrTh.Application.Common.Exceptions;
using PrTh.Domain;

namespace PrTh.Application.Features.Themes.Queries.GetThemeList
{
    public class GetThemeListQueryHandler
        : IRequestHandler<GetThemeListQuery, ThemeListVm>
    {
        private readonly IPrThDbContext _context;
        private readonly IMapper _mapper;

        public GetThemeListQueryHandler
            (IPrThDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<ThemeListVm> Handle(GetThemeListQuery request,
            CancellationToken cancellationToken)
        {
            await checkLanguageAsync(request.LanguageId, cancellationToken);

            var themeDtos = new List<ThemeLookupDto>();
            var themes = await getThemesAsync(cancellationToken);
            foreach(var theme in themes)
            {
                var translation = await getThemeTranslationAsync
                    (theme.Id, request.LanguageId, cancellationToken);
                var themeDto = _mapper.Map<ThemeLookupDto>(translation);
                themeDtos.Add(themeDto);
            }

            foreach(var themeDto in themeDtos)
            {
                themeDto.Chapters = await getChapterDtosAsync
                    (themeDto.Id, request.LanguageId, cancellationToken);
            }

            return new ThemeListVm { Themes = themeDtos };
        }

        private async Task checkLanguageAsync(Guid languageId,
            CancellationToken cancellationToken)
        {
            _ = await _context.Languages
                .FirstOrDefaultAsync(l => l.Id == languageId, cancellationToken)
                ?? throw new NotFoundException(nameof(Language), languageId);
        }

        private async Task<List<Theme>> getThemesAsync
            (CancellationToken cancellationToken)
        {
            return await _context.Themes
                .OrderBy(t => t.Number)
                .ToListAsync(cancellationToken);
        }

        private async Task<ThemeTranslation> getThemeTranslationAsync
            (Guid themeId, Guid languageId, CancellationToken cancellationToken)
        {
            var translation = await _context.ThemeTranslations
                .FirstOrDefaultAsync(t => t.ThemeId == themeId
                && t.LanguageId == languageId, cancellationToken);
            if (translation == null)
            {
                translation = new ThemeTranslation
                {
                    ThemeId = themeId,
                    LanguageId = languageId,
                    TitleTranslation = "-"
                };
                _context.ThemeTranslations.Add(translation);
            }
            return translation;
        }

        private async Task<List<ChapterLookupDto>> getChapterDtosAsync
            (Guid themeId, Guid languageId, CancellationToken cancellationToken)
        {
            var chapterDtos = new List<ChapterLookupDto>();
            var chapters = await getChaptersAsync(themeId, cancellationToken);
            foreach(var chapter in chapters)
            {
                var translation = await getChapterTranslationAsync
                    (chapter.Id, languageId, cancellationToken);
                var chapterDto = _mapper.Map<ChapterLookupDto>(translation);
                chapterDtos.Add(chapterDto);
            }
            return chapterDtos;
        }

        private async Task<List<Chapter>> getChaptersAsync
            (Guid themeId, CancellationToken cancellationToken)
        {
            return await _context.Chapters
                    .Where(c => c.ThemeId == themeId)
                    .OrderBy(c => c.Number)
                    .ToListAsync(cancellationToken);
        }

        private async Task<ChapterTranslation> getChapterTranslationAsync
            (Guid chapterId, Guid languageId, CancellationToken cancellationToken)
        {
            var translation = await _context.ChapterTranslations
                .FirstOrDefaultAsync(t => t.ChapterId == chapterId
                && t.LanguageId == languageId, cancellationToken);
            if (translation == null)
            {
                translation = new ChapterTranslation
                {
                    ChapterId = chapterId,
                    LanguageId = languageId,
                    TitleTranslation = "-"
                };
                _context.ChapterTranslations.Add(translation);
            }
            return translation;
        }
    }
}
