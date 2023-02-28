using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PrTh.Application.Features.TermTranslations.Commands.UpdateTermTranslation;
using PrTh.WebApi.Models;

namespace PrTh.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TermTranslationsController : BaseController
    {
        private readonly IMapper _mapper;

        public TermTranslationsController(IMapper mapper) =>
            _mapper = mapper;

        [HttpPut]
        public async Task<IActionResult> Update
            ([FromBody] UpdateTermTranslationDto updateTermTranslationDto)
        {
            var command = _mapper
                .Map<UpdateTermTranslationCommand>(updateTermTranslationDto);
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
