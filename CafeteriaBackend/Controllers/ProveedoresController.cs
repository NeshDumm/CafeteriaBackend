using CafeteriaBackend.Interfaces;
using CafeteriaBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeteriaBackend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProveedoresController : ControllerBase
{
    private readonly IProveedorService _service;

    // Inyección de dependencias en el controller solo del service, no del repository
    public ProveedoresController(IProveedorService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Roles = "Admin, Usuario")]
    public async Task<ActionResult<IEnumerable<Proveedor>>> ObtenerTodos()
    {
        var proveedores = await _service.ObtenerTodosAsync();
        return Ok(proveedores);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, Usuario")]
    public async Task<ActionResult<Proveedor>> ObtenerPorId(int id)
    {
        var proveedor = await _service.ObtenerPorIdAsync(id);
        if (proveedor == null)
            return NotFound(new { mensaje = "Proveedor no encontrado" });

        return Ok(proveedor);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Crear([FromBody] Proveedor proveedor)
    {
        try
        {
            var creado = await _service.CrearProveedorAsync(proveedor);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Actualizar(int id, [FromBody] Proveedor proveedor)
    {
        proveedor.Id = id;
        bool actualizado = await _service.ActualizarProveedorAsync(proveedor);
        if (!actualizado)
            return NotFound(new { mensaje = "Proveedor no encontrado" });

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Eliminar(int id)
    {
        bool eliminado = await _service.EliminarProveedorAsync(id);
        if (!eliminado)
            return NotFound(new { mensaje = "Proveedor no encontrado" });

        return NoContent();
    }
}