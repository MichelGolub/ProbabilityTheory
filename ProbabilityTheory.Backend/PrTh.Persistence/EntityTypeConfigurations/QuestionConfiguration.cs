using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrTh.Domain;

namespace PrTh.Persistence.EntityTypeConfigurations
{
    public class QuestionConfiguration
        : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(q => q.Id);
            builder.HasIndex(q => q.Id).IsUnique();
            builder.HasOne<Chapter>()
                .WithMany()
                .HasForeignKey(q => q.ChapterId);
            builder.Property(q => q.Number).IsRequired();
        }
    }
}
