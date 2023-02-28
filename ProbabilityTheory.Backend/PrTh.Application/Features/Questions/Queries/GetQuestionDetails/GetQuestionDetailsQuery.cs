using MediatR;

namespace PrTh.Application.Features.Questions.Queries.GetQuestionDetails
{
    public class GetQuestionDetailsQuery
        : IRequest<QuestionDetailsVm>
    {
        public Guid Id { get; set; }
        public Guid LanguageId { get; set; }
    }
}
