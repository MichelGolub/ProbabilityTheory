using PrTh.Application.Common.Mappings;
using PrTh.Application.Features.Steps.Commands.CreateStep;

namespace PrTh.WebApi.Models
{
    public class CreateStepDto : IMapWith<CreateStepCommand>
    {
        public Guid ExerciseId { get; set; }

        public void Mapping(AssemblyMappingProfile profile)
        {
            profile.CreateMap<CreateStepDto, CreateStepCommand>()
                .ForMember(command => command.ExerciseId,
                    opt => opt.MapFrom(dto => dto.ExerciseId));
        }
    }
}
