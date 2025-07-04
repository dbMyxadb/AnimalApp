using finallexamp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace finallexamp.DAL.Configurations
{
    public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.ScientificName).HasMaxLength(200);
            builder.Property(a => a.ConservationStatus).HasMaxLength(50);
            builder.Property(a => a.GroupName).HasMaxLength(50);
            builder.Property(a => a.CountryCode).HasMaxLength(10);
        }
    }
}
