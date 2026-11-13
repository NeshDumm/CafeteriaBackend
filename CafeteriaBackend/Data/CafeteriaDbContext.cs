using CafeteriaBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace CafeteriaBackend.Data;

public class CafeteriaDbContext : DbContext
{
    public CafeteriaDbContext(DbContextOptions<CafeteriaDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Proveedor> Proveedores => Set<Proveedor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            // Le decimos a EF que el Nombre tendrá máximo 100 caracteres, igual que en la práctica
            entity.Property(p => p.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.Property(p => p.Nombre).HasMaxLength(100);
            entity.Property(p => p.Telefono).HasMaxLength(20);
        });
    }
}