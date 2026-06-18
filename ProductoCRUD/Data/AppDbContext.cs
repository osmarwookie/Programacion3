using Microsoft.EntityFrameworkCore;
using ProductoCRUD.Models;

namespace ProductoCRUD.Data;

public class AppDbContext : DbContext
{
    public DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=ProductoCRUD.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.Property(p => p.Nombre).HasMaxLength(200).IsRequired();
            entity.Property(p => p.Precio).HasColumnType("decimal(18,2)");
        });
    }
}