using MediatR;

namespace PrTh.Application.Features.Themes.Commands.DeleteTheme
{
    public class DeleteThemeCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
