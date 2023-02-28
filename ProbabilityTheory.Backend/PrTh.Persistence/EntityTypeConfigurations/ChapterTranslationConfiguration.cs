using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrTh.Domain;

namespace PrTh.Persistence.EntityTypeConfigurations
{
    public class ChapterTranslationConfiguration : IEntityTypeConfiguration<ChapterTranslation>
    {
        public void Configure(EntityTypeBuilder<ChapterTranslation> builder)
        {
            builder.HasKey(ct => new { ct.ChapterId, ct.LanguageId });
            builder.HasIndex(ct => new { ct.ChapterId, ct.LanguageId }).IsUnique();
            builder.HasOne<Chapter>()
                .WithMany()
                .HasForeignKey(c => c.ChapterId);
            builder.HasOne<Language>()
                .WithMany()
                .HasForeignKey(ct => ct.LanguageId);
            builder.Property(ct => ct.TitleTranslation)
                .HasDefaultValue("-")
                .IsRequired();
        }
    }
}
