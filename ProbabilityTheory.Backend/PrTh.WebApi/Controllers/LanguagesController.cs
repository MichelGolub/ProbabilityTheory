using Microsoft.AspNetCore.Mvc;
using PrTh.Application.Features.Languages.Queries.GetLanguageList;

namespace PrTh.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class LanguagesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<LanguageListVm>> GetAll()
        {
            var query = new GetLanguageListQuery { };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}
