using PrTh.Application.Common.Mappings;
using PrTh.Application.Features.Exercises.Commands.CreateExercise;

namespace PrTh.WebApi.Models
{
    public class CreateExerciseDto 
        : IMapWith<CreateExerciseCommand>
    {
        public Guid ChapterId { get; set; }

        public void Mapping(AssemblyMappingProfile profile)
        {
            profile.CreateMap<CreateExerciseDto, CreateExerciseCommand>()
                .ForMember(command => command.ChapterId,
                    opt => opt.MapFrom(dto => dto.ChapterId));
        }
    }
}
