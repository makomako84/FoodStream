using Foodstream.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foodstream.Infrastructure;

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