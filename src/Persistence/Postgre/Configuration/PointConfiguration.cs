using Foodstream.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foodstream.Persistence;

public class PointConfiguration : IEntityTypeConfiguration<Point>
{
    public void Configure(EntityTypeBuilder<Point> builder)
    {
        builder.ToTable("point");
        builder.Property(p => p.Id)
            .HasColumnName("id")
            .IsRequired()
            .ValueGeneratedOnAdd();
    }
}