using AutoMapper;
using PrTh.Application.Common.Mappings;
using PrTh.Domain;

namespace PrTh.Application.Features.Themes.Queries.GetThemeList
{
    public class ThemeLookupDto : IMapWith<ThemeTranslation>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public IList<ChapterLookupDto> Chapters { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ThemeTranslation, ThemeLookupDto>()
                .ForMember(themeDto => themeDto.Id,
                    opt => opt.MapFrom(tt => tt.ThemeId))
                .ForMember(themeDto => themeDto.Title,
                    opt => opt.MapFrom(tt => tt.TitleTranslation));
        }
    }

    public class ChapterLookupDto : IMapWith<ChapterTranslation>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public void Mapping(AssemblyMappingProfile profile)
        {
            profile.CreateMap<ChapterTranslation, ChapterLookupDto>()
                .ForMember(dto => dto.Id,
                    opt => opt.MapFrom(ct => ct.ChapterId))
                .ForMember(dto => dto.Title,
                    opt => opt.MapFrom(ct => ct.TitleTranslation));
        }
    }
}