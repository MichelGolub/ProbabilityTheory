using Microsoft.AspNetCore.Mvc;
using PrTh.Application.Features.Terms.Commands.CreateTerm;
using PrTh.Application.Features.Terms.Commands.DeleteTerm;
using PrTh.Application.Features.Terms.Queries.GetTermDetails;
using PrTh.Application.Features.Terms.Queries.GetTermList;

namespace PrTh.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TermsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<TermListVm>> GetAll
            ([FromQuery] Guid languageId)
        {
            var query = new GetTermListQuery { LanguageId = languageId };
            var terms = await Mediator.Send(query);
            return Ok(terms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TermDetailsVm>> Get
            (Guid id, [FromQuery] Guid languageId)
        {
            var query = new GetTermDetailsQuery
            {
                TermId = id,
                LanguageId = languageId
            };
            var term = await Mediator.Send(query);

            return Ok(term);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create()
        {
            var command = new CreateTermCommand { };
            var termId = await Mediator.Send(command);
            return Ok(termId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteTermCommand { Id = id };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
