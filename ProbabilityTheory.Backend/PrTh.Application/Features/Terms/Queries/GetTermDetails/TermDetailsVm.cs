using AutoMapper;
using PrTh.Application.Common.Mappings;
using PrTh.Domain;

namespace PrTh.Application.Features.Terms.Queries.GetTermDetails
{
    public class TermDetailsVm 
        : IMapWith<TermTranslation>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Definition { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TermTranslation, TermDetailsVm>()
                .ForMember(vm => vm.Id,
                    opt => opt.MapFrom(tt => tt.TermId))
                .ForMember(vm => vm.Title,
                    opt => opt.MapFrom(tt => tt.TitleTranslation))
                .ForMember(vm => vm.Definition,
                    opt => opt.MapFrom(tt => tt.DefinitionTranslation));
        }
    }
}