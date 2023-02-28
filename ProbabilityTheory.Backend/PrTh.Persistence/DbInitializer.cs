namespace PrTh.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(PrThDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
