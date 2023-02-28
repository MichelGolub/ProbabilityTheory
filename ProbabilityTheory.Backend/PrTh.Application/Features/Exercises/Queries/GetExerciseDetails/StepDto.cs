using AutoMapper;
using PrTh.Application.Common.Mappings;
using PrTh.Domain;

namespace PrTh.Application.Features.Exercises.Queries.GetExerciseDetails
{
    public class StepDto 
        : IMapWith<StepTranslation>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StepTranslation, StepDto>()
                .ForMember(dto => dto.Id,
                    opt => opt.MapFrom(st => st.StepId))
                .ForMember(dto => dto.Description,
                    opt => opt.MapFrom(st => st.DescriptionTranslation));
        }
    }
}
