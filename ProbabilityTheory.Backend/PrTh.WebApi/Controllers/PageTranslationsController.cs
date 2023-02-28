using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PrTh.Application.Features.PageTranslations.Commands.UpdatePageTranslation;
using PrTh.WebApi.Models;

namespace PrTh.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PageTranslationsController
        : BaseController
    {
        private readonly IMapper _mapper;

        public PageTranslationsController
            (IMapper mapper) => _mapper = mapper;

        [HttpPut]
        public async Task<IActionResult> Update
            ([FromBody] UpdatePageTranslationDto updatePageTranslationDto)
        {
            var command = _mapper.Map<UpdatePageTranslationCommand>
                (updatePageTranslationDto);
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
