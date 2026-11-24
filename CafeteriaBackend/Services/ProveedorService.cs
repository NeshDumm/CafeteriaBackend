using CafeteriaBackend.Interfaces;
using CafeteriaBackend.Models;

namespace CafeteriaBackend.Services;

public class ProveedorService : IProveedorService
{
    private readonly IProveedorRepository _repository;

    public ProveedorService(IProveedorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Proveedor>> ObtenerTodosAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Proveedor?> ObtenerPorIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Proveedor> CrearProveedorAsync(Proveedor proveedor)
    {
        // Validamos primero que el nombre del proveedor no esté vacío o nulo
        if (string.IsNullOrWhiteSpace(proveedor.Nombre))
        {
            throw new ArgumentException("El nombre del proveedor es obligatorio.");
        }

        await _repository.AddAsync(proveedor);
        await _repository.SaveChangesAsync();
        return proveedor;
    }

    public async Task<bool> ActualizarProveedorAsync(Proveedor proveedor)
    {
        var actualizado = await _repository.UpdateAsync(proveedor);
        if (actualizado)
        {
            await _repository.SaveChangesAsync();
        }
        return actualizado;
    }

    public async Task<bool> EliminarProveedorAsync(int id)
    {
        var eliminado = await _repository.DeleteAsync(id);
        if (eliminado)
        {
            await _repository.SaveChangesAsync();
        }
        return eliminado;
    }
}