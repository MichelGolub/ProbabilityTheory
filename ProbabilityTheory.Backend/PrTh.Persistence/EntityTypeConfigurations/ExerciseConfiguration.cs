using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrTh.Domain;

namespace PrTh.Persistence.EntityTypeConfigurations
{
    public class ExerciseConfiguration 
        : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();
            builder.HasOne<Chapter>()
                .WithMany()
                .HasForeignKey(e => e.ChapterId);
            builder.Property(e => e.Number).IsRequired();
        }
    }
}
