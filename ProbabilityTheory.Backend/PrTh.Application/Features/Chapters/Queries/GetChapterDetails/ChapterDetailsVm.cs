using AutoMapper;
using PrTh.Application.Common.Mappings;
using PrTh.Domain;

namespace PrTh.Application.Features.Chapters.Queries.GetChapterDetails
{
    public class ChapterDetailsVm 
        : IMapWith<Chapter>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid ThemeId { get; set; }
        public IEnumerable<PageLookupDto> Pages { get; set; }
        public IEnumerable<QuestionLookupDto> Questions { get; set; }
        public IEnumerable<ExerciseLookupDto> Exercises { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Chapter, ChapterDetailsVm>()
                .ForMember(chapterVm => chapterVm.Id,
                    opt => opt.MapFrom(chapter => chapter.Id))
                .ForMember(chapterVm => chapterVm.ThemeId,
                    opt => opt.MapFrom(chapter => chapter.ThemeId));
        }
    }

    public class PageLookupDto : IMapWith<Page>
    {
        public Guid Id { get; set; }

        public void Mapping(AssemblyMappingProfile profile)
        {
            profile.CreateMap<Page, PageLookupDto>()
                .ForMember(dto => dto.Id,
                    opt => opt.MapFrom(p => p.Id));
        }
    }

    public class QuestionLookupDto : IMapWith<Question>
    {
        public Guid Id { get; set; }

        public void Mapping(AssemblyMappingProfile profile)
        {
            profile.CreateMap<Question, QuestionLookupDto>()
                .ForMember(dto => dto.Id,
                    opt => opt.MapFrom(q => q.Id));
        }
    }

    public class ExerciseLookupDto : IMapWith<Exercise>
    {
        public Guid Id { get; set; }

        public void Mapping(AssemblyMappingProfile profile)
        {
            profile.CreateMap<Exercise, ExerciseLookupDto>()
                .ForMember(dto => dto.Id,
                    opt => opt.MapFrom(q => q.Id));
        }
    }
}