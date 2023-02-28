using AutoMapper;
using PrTh.Application.Common.Mappings;
using PrTh.Domain;

namespace PrTh.Application.Features.Terms.Queries.GetTermList
{
    public class TermListVm
    {
        public IList<TermLookupDto> Terms { get; set; }
    }

    public class TermLookupDto : IMapWith<TermTranslation>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TermTranslation, TermLookupDto>()
                .ForMember(dto => dto.Id,
                    opt => opt.MapFrom(t => t.TermId))
                .ForMember(dto => dto.Title,
                    opt => opt.MapFrom(t => t.TitleTranslation));
        }
    }
}