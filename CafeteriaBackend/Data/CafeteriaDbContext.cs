using CafeteriaBackend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CafeteriaBackend.Data;

public class CafeteriaDbContext : IdentityDbContext<ApplicationUser>
{
    public CafeteriaDbContext(DbContextOptions<CafeteriaDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // NUEVA: siempre primero

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(p => p.Nombre).HasMaxLength(100);
        });
    }
}