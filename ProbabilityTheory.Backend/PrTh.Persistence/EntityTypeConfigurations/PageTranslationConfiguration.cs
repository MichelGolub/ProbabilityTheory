using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrTh.Domain;

namespace PrTh.Persistence.EntityTypeConfigurations
{
    public class PageTranslationConfiguration : IEntityTypeConfiguration<PageTranslation>
    {
        public void Configure(EntityTypeBuilder<PageTranslation> builder)
        {
            builder.HasKey(pt => new { pt.PageId, pt.LanguageId });
            builder.HasIndex(pt => new { pt.PageId, pt.LanguageId }).IsUnique();
            builder.HasOne<Page>()
                .WithMany()
                .HasForeignKey(pt => pt.PageId);
            builder.HasOne<Language>()
                .WithMany()
                .HasForeignKey(pt => pt.LanguageId);
            builder.Property(pt => pt.ContentTranslation)
                .HasDefaultValue("-")
                .IsRequired();
        }
    }
}
