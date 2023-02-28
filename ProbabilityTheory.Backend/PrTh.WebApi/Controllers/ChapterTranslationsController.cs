using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PrTh.Application.Features.ChapterTranslations.Commands.UpdateChapterTranslation;
using PrTh.WebApi.Models;

namespace PrTh.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ChapterTranslationsController : BaseController
    {
        private readonly IMapper _mapper;

        public ChapterTranslationsController
            (IMapper mapper) => _mapper = mapper;

        [HttpPut]
        public async Task<IActionResult> Update
            ([FromBody] UpdateChapterTranslationDto updateChapterTranslationDto)
        {
            var command = _mapper.Map<UpdateChapterTranslationCommand>
                (updateChapterTranslationDto);
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
