using CafeteriaBackend.Data;
using CafeteriaBackend.Models;
using Microsoft.EntityFrameworkCore;
using CafeteriaBackend.Interfaces;

namespace CafeteriaBackend.Repositories
{
    public class ProveedorRepositorio : IProveedorRepositorio
    {
        private readonly CafeteriaDbContext _context;
        public ProveedorRepositorio(CafeteriaDbContext context)
        {
            _context = context;
        }
        public async Task<List<Proveedor>> ObtenerTodosAsync()
        {
            return await _context.Proveedores.AsNoTracking().ToListAsync();
        }
        public async Task<Proveedor?> BuscarPorIdAsync(int id)
        {
            return await _context.Proveedores
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Proveedor> AgregarAsync(Proveedor proveedor)
        {
            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }
        public async Task<bool> ActualizarAsync(Proveedor proveedor)
        {
            var existente = await _context.Proveedores.FindAsync(proveedor.Id);
            if (existente == null) return false;
            existente.Nombre = proveedor.Nombre;
            existente.Ruc = proveedor.Ruc;
            existente.Telefono = proveedor.Telefono;
            existente.Direccion = proveedor.Direccion;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EliminarPorIdAsync(int id)
        {
            var existente = await _context.Proveedores.FindAsync(id);
            if (existente == null) return false;
            _context.Proveedores.Remove(existente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
