namespace PrTh.Domain
{
    public class ExerciseTranslation
    {
        public Guid ExerciseId { get; set; }
        public Guid LanguageId { get; set; }
        public string DescriptionTranslation { get; set; }
    }
}
