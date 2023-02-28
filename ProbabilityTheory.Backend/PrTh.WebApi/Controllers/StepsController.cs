using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PrTh.Application.Features.Steps.Commands.CreateStep;
using PrTh.Application.Features.Steps.Commands.DeleteStep;
using PrTh.WebApi.Models;

namespace PrTh.WebApi.Controllers
{

    [Route("api/[controller]")]
    public class StepsController : BaseController
    {
        private readonly IMapper _mapper;

        public StepsController(IMapper mapper) =>
            _mapper = mapper;

        [HttpPost]
        public async Task<ActionResult<Guid>> Create
            ([FromBody] CreateStepDto createStepDto)
        {
            var command = _mapper.Map<CreateStepCommand>(createStepDto);

            var stepId = await Mediator.Send(command);
            return Ok(stepId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteStepCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
