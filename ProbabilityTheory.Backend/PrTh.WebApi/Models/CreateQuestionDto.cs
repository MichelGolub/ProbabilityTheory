using PrTh.Application.Common.Mappings;
using PrTh.Application.Features.Questions.Commands.CreateQuestion;

namespace PrTh.WebApi.Models
{
    public class CreateQuestionDto
        : IMapWith<CreateQuestionCommand>
    {
        public Guid ChapterId { get; set; }

        public void Mapping(AssemblyMappingProfile profile)
        {
            profile.CreateMap<CreateQuestionDto, CreateQuestionCommand>()
                .ForMember(command => command.ChapterId,
                    opt => opt.MapFrom(dto => dto.ChapterId));
        }
    }
}
