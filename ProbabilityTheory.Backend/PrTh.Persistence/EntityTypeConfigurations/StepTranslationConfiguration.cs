using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrTh.Domain;

namespace PrTh.Persistence.EntityTypeConfigurations
{
    public class StepTranslationConfiguration 
        : IEntityTypeConfiguration<StepTranslation>
    {
        public void Configure(EntityTypeBuilder<StepTranslation> builder)
        {
            builder.HasKey(st => new { st.StepId, st.LanguageId });
            builder.HasIndex(st => new { st.StepId, st.LanguageId }).IsUnique();
            builder.HasOne<Step>()
                    .WithMany()
                    .HasForeignKey(st => st.StepId);
            builder.HasOne<Language>()
                    .WithMany()
                    .HasForeignKey(st => st.LanguageId);
            builder.Property(st => st.DescriptionTranslation)
                .HasDefaultValue("-")
                .IsRequired();
        }
    }
}
