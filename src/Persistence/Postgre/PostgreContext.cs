using Foodstream.Domain;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Linq;

namespace Foodstream.Persistence.Postgre;

public class PostgreContext : DbContext
{
    public DbSet<Point> Points { get; set; }

    public PostgreContext(DbContextOptions<PostgreContext> options) : base(options)
    {
        // .. initializing events here ..
    }

    static PostgreContext()
    {
        // .. enums here ..
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PointConfiguration());
    }
}
