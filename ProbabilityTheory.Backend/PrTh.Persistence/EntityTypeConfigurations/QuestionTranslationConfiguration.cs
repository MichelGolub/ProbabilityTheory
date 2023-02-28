using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrTh.Domain;

namespace PrTh.Persistence.EntityTypeConfigurations
{
    public class QuestionTranslationConfiguration
        : IEntityTypeConfiguration<QuestionTranslation>
    {
        public void Configure(EntityTypeBuilder<QuestionTranslation> builder)
        {
            builder.HasKey(qt => new { qt.QuestionId, qt.LanguageId });
            builder.HasIndex(qt => new { qt.QuestionId, qt.LanguageId }).IsUnique();
            builder.HasOne<Question>()
                .WithMany()
                .HasForeignKey(qt => qt.QuestionId);
            builder.HasOne<Language>()
                .WithMany()
                .HasForeignKey(qt => qt.LanguageId);
            builder.Property(qt => qt.DescriptionTranslation)
                .HasDefaultValue("-")
                .IsRequired();
            builder.Property(qt => qt.AnswerTranslation)
                .HasDefaultValue("-")
                .IsRequired();
        }
    }
}
