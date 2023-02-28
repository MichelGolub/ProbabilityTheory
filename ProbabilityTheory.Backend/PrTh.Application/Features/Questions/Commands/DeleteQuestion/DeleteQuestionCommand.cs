using MediatR;

namespace PrTh.Application.Features.Questions.Commands.DeleteQuestion
{
    public class DeleteQuestionCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
