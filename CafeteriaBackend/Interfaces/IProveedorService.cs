using CafeteriaBackend.Models;

namespace CafeteriaBackend.Interfaces;

public interface IProveedorService
{
    Task<IEnumerable<Proveedor>> ObtenerTodosAsync();
    Task<Proveedor?> ObtenerPorIdAsync(int id);
    Task<Proveedor> CrearProveedorAsync(Proveedor proveedor);
    Task<bool> ActualizarProveedorAsync(Proveedor proveedor);
    Task<bool> EliminarProveedorAsync(int id);
}