using CafeteriaBackend.Models;
using CafeteriaBackend.Repositories;
using Microsoft.AspNetCore.Authorization; // 👈 1. Importar Authorization
using Microsoft.AspNetCore.Mvc;
using CafeteriaBackend.Interfaces;


namespace CafeteriaBackend.Controllers;

[Authorize] // 👈 2. Exige que CUALQUIER petición a este controlador tenga un token JWT válido
[ApiController]
[Route("api/[controller]")]
public class ProveedorController : ControllerBase
{
    private readonly IProveedorRepositorio _repo;

    public ProveedorController(IProveedorRepositorio repo)
    {
        _repo = repo;
    }

    [HttpGet]
    [Authorize(Roles = "Admin, Usuario")] // 👈 Permitido para ambos roles
    public async Task<ActionResult<List<Proveedor>>> ObtenerTodos()
    {
        var proveedores = await _repo.ObtenerTodosAsync();
        return Ok(proveedores);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, Usuario")] // 👈 Permitido para ambos roles
    public async Task<ActionResult<Proveedor>> ObtenerPorId(int id)
    {
        var proveedor = await _repo.BuscarPorIdAsync(id);
        if (proveedor == null) return NotFound(new { mensaje = "Proveedor no encontrado" });

        return Ok(proveedor);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")] // 👈 Solo el Admin puede crear
    public async Task<ActionResult> Crear([FromBody] Proveedor proveedor)
    {
        await _repo.AgregarAsync(proveedor);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = proveedor.Id }, proveedor);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")] // 👈 Solo el Admin puede modificar
    public async Task<ActionResult> Actualizar(int id, [FromBody] Proveedor proveedor)
    {
        proveedor.Id = id;
        bool actualizado = await _repo.ActualizarAsync(proveedor);
        if (!actualizado) return NotFound(new { mensaje = "Proveedor no encontrado" });

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")] // 👈 Solo el Admin puede eliminar
    public async Task<ActionResult> Eliminar(int id)
    {
        bool eliminado = await _repo.EliminarPorIdAsync(id);
        if (!eliminado) return NotFound(new { mensaje = "Proveedor no encontrado" });

        return NoContent();
    }
}