using PrTh.Application.Common.Mappings;
using PrTh.Application.Features.Chapters.Commands.CreateChapter;

namespace PrTh.WebApi.Models
{
    public class CreateChapterDto 
        : IMapWith<CreateChapterCommand>
    {
        public Guid ThemeId { get; set; }

        public void Mapping(AssemblyMappingProfile profile)
        {
            profile.CreateMap<CreateChapterDto, CreateChapterCommand>()
                .ForMember(command => command.ThemeId,
                    opt => opt.MapFrom(dto => dto.ThemeId));
        }
    }
}
