using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrTh.Domain;

namespace PrTh.Persistence.EntityTypeConfigurations
{
    public class PageConfiguration : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Id).IsUnique();
            builder.HasOne<Chapter>()
                .WithMany()
                .HasForeignKey(p => p.ChapterId);
            builder.Property(p => p.Number).IsRequired();
        }
    }
}
