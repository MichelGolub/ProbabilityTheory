using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PrTh.Application.Features.Pages.Commands.CreatePage;
using PrTh.Application.Features.Pages.Commands.DeletePage;
using PrTh.Application.Features.Pages.Queries.GetPageDetails;
using PrTh.WebApi.Models;

namespace PrTh.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PagesController : BaseController
    {
        private readonly IMapper _mapper;

        public PagesController
            (IMapper mapper) => _mapper = mapper;

        [HttpGet("{id}")]
        public async Task<ActionResult<PageDetailsVm>> Get
            (Guid id, [FromQuery] Guid languageId)
        {
            var query = new GetPageDetailsQuery
            {
                Id = id,
                LanguageId = languageId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create
            ([FromBody] CreatePageDto createPageDto)
        {
            var command = _mapper.Map<CreatePageCommand>(createPageDto);
            var pageId = await Mediator.Send(command);
            return Ok(pageId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeletePageCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
