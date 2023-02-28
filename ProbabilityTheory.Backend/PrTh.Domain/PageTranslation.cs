namespace PrTh.Domain
{
    public class PageTranslation
    {
        public Guid PageId { get; set; }
        public Guid LanguageId { get; set; }
        public string ContentTranslation { get; set; }
    }
}
