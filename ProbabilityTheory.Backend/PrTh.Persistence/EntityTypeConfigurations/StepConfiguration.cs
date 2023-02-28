using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrTh.Domain;

namespace PrTh.Persistence.EntityTypeConfigurations
{
    public class StepConfiguration 
        : IEntityTypeConfiguration<Step>
    {
        public void Configure(EntityTypeBuilder<Step> builder)
        {
            builder.HasKey(s => s.Id);
            builder.HasIndex(s => s.Id).IsUnique();
            builder.HasOne<Exercise>()
                .WithMany()
                .HasForeignKey(s => s.ExerciseId);
            builder.Property(s => s.Number).IsRequired();
        }
    }
}
