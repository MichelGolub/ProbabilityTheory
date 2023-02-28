using AutoMapper;
using PrTh.Application.Common.Mappings;
using PrTh.Domain;

namespace PrTh.Application.Features.Languages.Queries.GetLanguageList
{
    public class LanguageListVm
    {
        public IList<LanguageLookupDto> Languages { get; set; }
    }

    public class LanguageLookupDto : IMapWith<Language>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Language, LanguageLookupDto>()
                .ForMember(languageDto => languageDto.Id,
                    opt => opt.MapFrom(language => language.Id))
                .ForMember(languageDto => languageDto.Code,
                    opt => opt.MapFrom(language => language.Code));
        }
    }
}