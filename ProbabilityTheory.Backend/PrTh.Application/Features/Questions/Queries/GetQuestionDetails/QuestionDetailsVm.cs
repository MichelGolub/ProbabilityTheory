using AutoMapper;
using PrTh.Application.Common.Mappings;
using PrTh.Domain;

namespace PrTh.Application.Features.Questions.Queries.GetQuestionDetails
{
    public class QuestionDetailsVm 
        : IMapWith<QuestionTranslation>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; }
        public Guid ChapterId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<QuestionTranslation, QuestionDetailsVm>()
                .ForMember(vm => vm.Id,
                    opt => opt.MapFrom(qt => qt.QuestionId))
                .ForMember(vm => vm.Description,
                    opt => opt.MapFrom(qt => qt.DescriptionTranslation))
                .ForMember(vm => vm.Answer,
                    opt => opt.MapFrom(qt => qt.AnswerTranslation));
        }
    }
}