using MediatR;

namespace PrTh.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommand
        : IRequest<Guid>
    {
        public Guid ChapterId { get; set; }
    }
}
