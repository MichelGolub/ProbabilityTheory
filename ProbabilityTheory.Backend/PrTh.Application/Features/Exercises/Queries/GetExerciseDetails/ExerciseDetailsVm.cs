using AutoMapper;
using PrTh.Application.Common.Mappings;
using PrTh.Domain;

namespace PrTh.Application.Features.Exercises.Queries.GetExerciseDetails
{
    public class ExerciseDetailsVm : IMapWith<Exercise>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid ChapterId { get; set; }
        public IEnumerable<StepDto> Steps { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Exercise, ExerciseDetailsVm>()
                .ForMember(vm => vm.Id,
                    opt => opt.MapFrom(e => e.Id))
                .ForMember(vm => vm.ChapterId,
                    opt => opt.MapFrom(e => e.ChapterId));
        }
    }
}