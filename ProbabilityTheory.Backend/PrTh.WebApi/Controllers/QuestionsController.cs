using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PrTh.Application.Features.Questions.Commands.CreateQuestion;
using PrTh.Application.Features.Questions.Commands.DeleteQuestion;
using PrTh.Application.Features.Questions.Queries.GetQuestionDetails;
using PrTh.WebApi.Models;

namespace PrTh.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class QuestionsController : BaseController
    {
        private readonly IMapper _mapper;

        public QuestionsController
            (IMapper mapper) => _mapper = mapper;

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDetailsVm>> Get
            (Guid id, [FromQuery] Guid languageId)
        {
            var query = new GetQuestionDetailsQuery
            {
                Id = id,
                LanguageId = languageId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create
            ([FromBody] CreateQuestionDto createPageDto)
        {
            var command = _mapper.Map<CreateQuestionCommand>(createPageDto);
            var questionId = await Mediator.Send(command);
            return Ok(questionId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteQuestionCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
