namespace PrTh.Domain
{
    public class TermTranslation
    {
        public Guid TermId { get; set; }
        public Guid LanguageId { get; set; }
        public string TitleTranslation { get; set; }
        public string DefinitionTranslation { get; set; }
    }
}
