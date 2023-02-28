using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrTh.Domain;

namespace PrTh.Persistence.EntityTypeConfigurations
{
    public class ExerciseTranslationConfiguration
        : IEntityTypeConfiguration<ExerciseTranslation>
    {
        public void Configure(EntityTypeBuilder<ExerciseTranslation> builder)
        {
            builder.HasKey(et => new { et.ExerciseId, et.LanguageId });
            builder.HasIndex(et => new { et.ExerciseId, et.LanguageId }).IsUnique();
            builder.HasOne<Exercise>()
                .WithMany()
                .HasForeignKey(et => et.ExerciseId);
            builder.HasOne<Language>()
                .WithMany()
                .HasForeignKey(et => et.LanguageId);
            builder.Property(et => et.DescriptionTranslation)
                .HasDefaultValue("-")
                .IsRequired();
        }
    }
}
