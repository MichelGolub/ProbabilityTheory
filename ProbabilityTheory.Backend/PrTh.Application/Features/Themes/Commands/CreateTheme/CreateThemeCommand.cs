using MediatR;

namespace PrTh.Application.Features.Themes.Commands.CreateTheme
{
    public class CreateThemeCommand 
        : IRequest<Guid> { }
}
