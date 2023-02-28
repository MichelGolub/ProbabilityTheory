using Microsoft.AspNetCore.Mvc;
using PrTh.Application.Features.Themes.Commands.CreateTheme;
using PrTh.Application.Features.Themes.Commands.DeleteTheme;
using PrTh.Application.Features.Themes.Queries.GetThemeList;

namespace PrTh.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ThemesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ThemeListVm>> GetAll
            (Guid languageId)
        {
            var themesVm = await Mediator
                .Send(new GetThemeListQuery { LanguageId = languageId });
            return Ok(themesVm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create()
        {
            var command = new CreateThemeCommand { };
            var themeId = await Mediator.Send(command);
            return Ok(themeId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteThemeCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
