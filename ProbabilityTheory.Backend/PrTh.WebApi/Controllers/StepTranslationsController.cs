using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PrTh.Application.Features.StepTranslations.Commands.UpdateStepTranslation;
using PrTh.WebApi.Models;

namespace PrTh.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class StepTranslationsController : BaseController
    {
        private readonly IMapper _mapper;

        public StepTranslationsController(IMapper mapper) =>
            _mapper = mapper;

        [HttpPut]
        public async Task<IActionResult> Update
            ([FromBody] UpdateStepTranslationDto updateStepTranslationDto)
        {
            var command = _mapper.Map<UpdateStepTranslationCommand>
                (updateStepTranslationDto);
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
