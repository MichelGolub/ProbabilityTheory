using AutoMapper;
using PrTh.Application.Common.Mappings;
using PrTh.Domain;

namespace PrTh.Application.Features.Pages.Queries.GetPageDetails
{
    public class PageDetailsVm : IMapWith<Page>
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid ChapterId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Page, PageDetailsVm>()
                .ForMember(vm => vm.Id,
                    opt => opt.MapFrom(p => p.Id))
                .ForMember(vm => vm.ChapterId,
                    opt => opt.MapFrom(p => p.ChapterId));
        }
    }
}