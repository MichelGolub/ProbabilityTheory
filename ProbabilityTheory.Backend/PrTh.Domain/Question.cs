namespace PrTh.Domain
{
    public class Question
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public Guid ChapterId { get; set; }
    }
}
