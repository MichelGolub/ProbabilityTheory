using Microsoft.EntityFrameworkCore;
using PrTh.Domain;

namespace PrTh.Application.Interfaces
{
    public interface IPrThDbContext
    {
        public DbSet<Language> Languages { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<ThemeTranslation> ThemeTranslations { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<ChapterTranslation> ChapterTranslations { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageTranslation> PageTranslations { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<TermTranslation> TermTranslations { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionTranslation> QuestionTranslations { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseTranslation> ExerciseTranslations { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<StepTranslation> StepTranslations { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
