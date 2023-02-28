using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrTh.Domain;

namespace PrTh.Persistence.EntityTypeConfigurations
{
    public class TermTranslationConfiguration : IEntityTypeConfiguration<TermTranslation>
    {
        public void Configure(EntityTypeBuilder<TermTranslation> builder)
        {
            builder.HasKey(tt => new { tt.TermId, tt.LanguageId });
            builder.HasIndex(tt => new { tt.TermId, tt.LanguageId }).IsUnique();
            builder.HasOne<Term>()
                .WithMany()
                .HasForeignKey(tt => tt.TermId);
            builder.HasOne<Language>()
                .WithMany()
                .HasForeignKey(tt => tt.LanguageId);
            builder.Property(tt => tt.TitleTranslation)
                .HasDefaultValue("-")
                .IsRequired();
            builder.Property(tt => tt.DefinitionTranslation)
                .HasDefaultValue("-")
                .IsRequired();
        }
    }
}
