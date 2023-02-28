using PrTh.Application.Common.Mappings;
using PrTh.Application.Features.Pages.Commands.CreatePage;

namespace PrTh.WebApi.Models
{
    public class CreatePageDto 
        : IMapWith<CreatePageCommand>
    {
        public Guid ChapterId { get; set; }

        public void Mapping(AssemblyMappingProfile profile)
        {
            profile.CreateMap<CreatePageDto, CreatePageCommand>()
                .ForMember(command => command.ChapterId,
                    opt => opt.MapFrom(dto => dto.ChapterId));
        }
    }
}
