namespace PrTh.Domain
{
    public class ChapterTranslation
    {
        public Guid ChapterId { get; set; }
        public Guid LanguageId { get; set; }
        public string TitleTranslation { get; set; }
    }
}
