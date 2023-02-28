using PrTh.Application.Common.Mappings;
using PrTh.Application.Features.ExerciseTranslations.Commands.UpdateExerciseTranslation;

namespace PrTh.WebApi.Models
{
    public class UpdateExerciseTranslationDto
        : IMapWith<UpdateExerciseTranslationCommand>
    {
        public Guid ExerciseId { get; set; }
        public Guid LanguageId { get; set; }
        public string Description { get; set; }

        public void Mapping(AssemblyMappingProfile profile)
        {
            profile.CreateMap<UpdateExerciseTranslationDto, UpdateExerciseTranslationCommand>()
                .ForMember(command => command.LanguageId,
                    opt => opt.MapFrom(dto => dto.LanguageId))
                .ForMember(command => command.ExerciseId,
                    opt => opt.MapFrom(dto => dto.ExerciseId))
                .ForMember(command => command.Description,
                    opt => opt.MapFrom(dto => dto.Description));
        }
    }
}
