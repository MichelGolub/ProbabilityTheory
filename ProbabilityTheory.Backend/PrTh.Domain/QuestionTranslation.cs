namespace PrTh.Domain
{
    public class QuestionTranslation
    {
        public Guid QuestionId { get; set; }
        public Guid LanguageId { get; set; }
        public string DescriptionTranslation { get; set; }
        public string AnswerTranslation { get; set; }
    }
}
