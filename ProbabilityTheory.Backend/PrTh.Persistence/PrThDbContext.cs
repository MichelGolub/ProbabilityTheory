using Microsoft.EntityFrameworkCore;
using PrTh.Application.Interfaces;
using PrTh.Domain;
using PrTh.Persistence.EntityTypeConfigurations;

namespace PrTh.Persistence
{
    public class PrThDbContext : DbContext, IPrThDbContext
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

        public PrThDbContext
            (DbContextOptions<PrThDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new LanguageConfiguration());
            builder.ApplyConfiguration(new ThemeConfiguration());
            builder.ApplyConfiguration(new ThemeTranslationConfiguration());
            builder.ApplyConfiguration(new ChapterConfiguration());
            builder.ApplyConfiguration(new ChapterTranslationConfiguration());
            builder.ApplyConfiguration(new PageConfiguration());
            builder.ApplyConfiguration(new PageTranslationConfiguration());
            builder.ApplyConfiguration(new TermConfiguration());
            builder.ApplyConfiguration(new TermTranslationConfiguration());
            builder.ApplyConfiguration(new QuestionConfiguration());
            builder.ApplyConfiguration(new QuestionTranslationConfiguration());
            builder.ApplyConfiguration(new ExerciseConfiguration());
            builder.ApplyConfiguration(new ExerciseTranslationConfiguration());
            builder.ApplyConfiguration(new StepConfiguration());
            builder.ApplyConfiguration(new StepTranslationConfiguration());

            builder.Entity<Language>().HasData(
                new Language[]
                {
                    new Language{ Id = Guid.NewGuid(), Code = "ukr" },
                    new Language{ Id = Guid.NewGuid(), Code = "eng" },
                });

            base.OnModelCreating(builder);
        }
    }
}