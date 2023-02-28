using MediatR;

namespace PrTh.Application.Features.Themes.Queries.GetThemeList
{
    public class GetThemeListQuery 
        : IRequest<ThemeListVm>
    {
        public Guid LanguageId { get; set; }
    }
}
