using CafeteriaBackend.Data;
using CafeteriaBackend.Models;

namespace CafeteriaBackend.Repositories
{
    public class ProveedorRepositorio
    {
        private readonly CafeteriaDbContext _context;


        public ProveedorRepositorio(CafeteriaDbContext context)
        {
            _context = context;
        }

        public async Task<Proveedor> AgregarAsync(Proveedor proveedor)
        {
            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }
    }
}
