using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrTh.Domain;

namespace PrTh.Persistence.EntityTypeConfigurations
{
    public class ThemeTranslationConfiguration : IEntityTypeConfiguration<ThemeTranslation>
    {
        public void Configure(EntityTypeBuilder<ThemeTranslation> builder)
        {
            builder.HasKey(tt => new { tt.ThemeId, tt.LanguageId });
            builder.HasIndex(tt => new { tt.ThemeId, tt.LanguageId }).IsUnique();
            builder.HasOne<Theme>()
                .WithMany()
                .HasForeignKey(tt => tt.ThemeId);
            builder.HasOne<Language>()
                .WithMany()
                .HasForeignKey(tt => tt.LanguageId);
            builder.Property(tt => tt.TitleTranslation)
                .HasDefaultValue("-")
                .IsRequired();
        }
    }
}
