using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PrTh.Application.Features.Exercises.Commands.CreateExercise;
using PrTh.Application.Features.Exercises.Commands.DeleteExercise;
using PrTh.Application.Features.Exercises.Queries.GetExerciseDetails;
using PrTh.WebApi.Models;

namespace PrTh.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ExercisesController : BaseController
    {
        private readonly IMapper _mapper;

        public ExercisesController(IMapper mapper) =>
            _mapper = mapper;

        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseDetailsVm>> Get
            (Guid id, [FromQuery] Guid languageId)
        {
            var query = new GetExerciseDetailsQuery
            {
                Id = id,
                LanguageId = languageId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create
            ([FromBody] CreateExerciseDto createExerciseDto)
        {
            var command = _mapper
                .Map<CreateExerciseCommand>(createExerciseDto);

            var exerciseId = await Mediator.Send(command);
            return Ok(exerciseId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteExerciseCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
