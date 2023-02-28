using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PrTh.Application.Features.QuestionTranslations.Commands.UpdateQuestionTranslation;
using PrTh.WebApi.Models;

namespace PrTh.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class QuestionTranslationsController
        : BaseController
    {
        private readonly IMapper _mapper;

        public QuestionTranslationsController
            (IMapper mapper) => _mapper = mapper;

        [HttpPut]
        public async Task<IActionResult> Update
            ([FromBody] UpdateQuestionTranslationDto updatePageTranslationDto)
        {
            var command = _mapper.Map<UpdateQuestionTranslationCommand>
                (updatePageTranslationDto);
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
