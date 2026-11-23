using CafeteriaBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace CafeteriaBackend.Interfaces
{
    public interface IProveedorRepositorio
    {
        Task<List<Proveedor>> ObtenerTodosAsync();
        Task<Proveedor?> BuscarPorIdAsync(int id);
        Task<Proveedor> AgregarAsync(Proveedor proveedor);
        Task<bool> ActualizarAsync(Proveedor proveedor);
        Task<bool> EliminarPorIdAsync(int id);
    }
}
