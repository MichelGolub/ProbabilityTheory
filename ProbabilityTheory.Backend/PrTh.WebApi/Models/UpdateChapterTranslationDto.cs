using PrTh.Application.Common.Mappings;
using PrTh.Application.Features.ChapterTranslations.Commands.UpdateChapterTranslation;

namespace PrTh.WebApi.Models
{
    public class UpdateChapterTranslationDto
        : IMapWith<UpdateChapterTranslationCommand>
    {
        public Guid ChapterId { get; set; }
        public Guid LanguageId { get; set; }
        public string Title { get; set; }

        public void Mapping(AssemblyMappingProfile profile)
        {
            profile.CreateMap<UpdateChapterTranslationDto,
                UpdateChapterTranslationCommand>()
                .ForMember(command => command.LanguageId,
                    opt => opt.MapFrom(dto => dto.LanguageId))
                .ForMember(command => command.ChapterId,
                    opt => opt.MapFrom(dto => dto.ChapterId))
                .ForMember(command => command.Title,
                    opt => opt.MapFrom(dto => dto.Title));
        }
    }
}
