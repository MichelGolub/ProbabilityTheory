using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Application.Interfaces;
using PrTh.Application.Common.Exceptions;
using PrTh.Domain;

namespace PrTh.Application.Features.Chapters.Queries.GetChapterDetails
{
    public class GetChapterDetailsQueryHandler
        : IRequestHandler<GetChapterDetailsQuery, ChapterDetailsVm>
    {
        private readonly IPrThDbContext _context;
        private readonly IMapper _mapper;

        public GetChapterDetailsQueryHandler(IPrThDbContext context,
            IMapper mapper) => (_context, _mapper) = (context, mapper);

        public async Task<ChapterDetailsVm> Handle(GetChapterDetailsQuery request,
            CancellationToken cancellationToken)
        {
            await checkLanguageAsync(request.LanguageId, cancellationToken);

            var chapter =  await getChapterAsync(request.Id, cancellationToken);
            var pages = await getChapterPageLookupDtosAsync
                (request.Id, cancellationToken);
            var questions = await getChapterQuestionLookupDtosAsync
                (request.Id, cancellationToken);
            var exercises = await getChapterExerciseLookupDtosAsync
                (request.Id, cancellationToken);
            var translation = await getChapterTranslationAsync
                (request.Id, request.LanguageId, cancellationToken);

            var chapterVm = _mapper.Map<ChapterDetailsVm>(chapter);
            chapterVm.Pages = pages;
            chapterVm.Questions = questions;
            chapterVm.Exercises = exercises;
            chapterVm.Title = translation.TitleTranslation;

            return chapterVm;
        }

        private async Task<Unit> checkLanguageAsync
            (Guid id, CancellationToken cancellationToken)
        {
            var _ = await _context.Languages
                .FirstOrDefaultAsync(l => l.Id == id, cancellationToken)
                ?? throw new NotFoundException(nameof(Language), id);
            return Unit.Value;
        }

        private async Task<Chapter> getChapterAsync
            (Guid id, CancellationToken cancellationToken)
        {
            return await _context.Chapters
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken)
                ?? throw new NotFoundException(nameof(Chapter), id);
        }

        private async Task<List<PageLookupDto>> getChapterPageLookupDtosAsync
            (Guid chapterId, CancellationToken cancellationToken)
        {
           return await _context.Pages
                .Where(p => p.ChapterId == chapterId)
                .OrderBy(p => p.Number)
                .ProjectTo<PageLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        private async Task<List<QuestionLookupDto>> getChapterQuestionLookupDtosAsync
            (Guid chapterId, CancellationToken cancellationToken)
        {
            return await _context.Questions
                 .Where(p => p.ChapterId == chapterId)
                 .OrderBy(p => p.Number)
                 .ProjectTo<QuestionLookupDto>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellationToken);
        }

        private async Task<List<ExerciseLookupDto>> getChapterExerciseLookupDtosAsync
            (Guid chapterId, CancellationToken cancellationToken)
        {
            return await _context.Exercises
                 .Where(e => e.ChapterId == chapterId)
                 .OrderBy(e => e.Number)
                 .ProjectTo<ExerciseLookupDto>(_mapper.ConfigurationProvider)
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
