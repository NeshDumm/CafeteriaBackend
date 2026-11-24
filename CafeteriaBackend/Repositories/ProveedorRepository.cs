using CafeteriaBackend.Data;
using CafeteriaBackend.Interfaces;
using CafeteriaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeteriaBackend.Repositories;

public class ProveedorRepository : IProveedorRepository
{
    private readonly CafeteriaDbContext _context;

    public ProveedorRepository(CafeteriaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Proveedor>> GetAllAsync()
    {
        return await _context.Proveedores.AsNoTracking().ToListAsync();
    }

    public async Task<Proveedor?> GetByIdAsync(int id)
    {
        return await _context.Proveedores
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Proveedor> AddAsync(Proveedor proveedor)
    {
        await _context.Proveedores.AddAsync(proveedor);
        return proveedor;
    }

    public async Task<bool> UpdateAsync(Proveedor proveedor)
    {
        var existente = await _context.Proveedores.FindAsync(proveedor.Id);
        if (existente == null) return false;

        existente.Nombre = proveedor.Nombre;
        existente.Ruc = proveedor.Ruc;
        existente.Telefono = proveedor.Telefono;
        existente.Direccion = proveedor.Direccion;
        existente.Activo = proveedor.Activo;

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existente = await _context.Proveedores.FindAsync(id);
        if (existente == null) return false;

        _context.Proveedores.Remove(existente);
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}