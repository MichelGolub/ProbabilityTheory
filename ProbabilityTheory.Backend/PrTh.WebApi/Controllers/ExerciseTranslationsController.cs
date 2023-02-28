using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PrTh.Application.Features.ExerciseTranslations.Commands.UpdateExerciseTranslation;
using PrTh.WebApi.Models;

namespace PrTh.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ExerciseTranslationsController : BaseController
    {
        private readonly IMapper _mapper;

        public ExerciseTranslationsController(IMapper mapper) =>
            _mapper = mapper;

        [HttpPut]
        public async Task<IActionResult> Update
            ([FromBody] UpdateExerciseTranslationDto updateExerciseTranslationDto)
        {
            var command = _mapper.Map<UpdateExerciseTranslationCommand>
                (updateExerciseTranslationDto);
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
