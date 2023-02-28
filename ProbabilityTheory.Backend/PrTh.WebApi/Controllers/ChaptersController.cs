using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PrTh.Application.Features.Chapters.Commands.CreateChapter;
using PrTh.Application.Features.Chapters.Commands.DeleteChapter;
using PrTh.Application.Features.Chapters.Queries.GetChapterDetails;
using PrTh.WebApi.Models;

namespace PrTh.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ChaptersController : BaseController
    {
        private readonly IMapper _mapper;

        public ChaptersController(IMapper mapper) =>
            _mapper = mapper;

        [HttpGet("{id}")]
        public async Task<ActionResult<ChapterDetailsVm>> Get
            (Guid id, [FromQuery] Guid languageId)
        {
            var query = new GetChapterDetailsQuery
            {
                Id = id,
                LanguageId = languageId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create
            ([FromBody] CreateChapterDto createChapterDto)
        {
            var command = _mapper.Map<CreateChapterCommand>(createChapterDto);

            var chapterId = await Mediator.Send(command);
            return Ok(chapterId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteChapterCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
