using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PrTh.Application.Features.ThemeTranslations.Commands.UpdateThemeTranslation;
using PrTh.WebApi.Models;

namespace PrTh.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ThemeTranslationsController : BaseController
    {
        private readonly IMapper _mapper;

        public ThemeTranslationsController
            (IMapper mapper) => _mapper = mapper;

        [HttpPut]
        public async Task<IActionResult> Update
            ([FromBody] UpdateThemeTranslationDto updateThemeTranslationDto)
        {
            var command = _mapper.Map<UpdateThemeTranslationCommand>
                (updateThemeTranslationDto);
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
