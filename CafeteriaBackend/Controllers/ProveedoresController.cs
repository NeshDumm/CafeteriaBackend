
using CafeteriaBackend.Models;
using CafeteriaBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeteriaBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedoresController : Controller
    {
        private readonly ProveedorRepositorio _repo;

        // Constructor: inicializa el repositorio
        public ProveedoresController(ProveedorRepositorio repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Agregar([FromBody] Proveedor dto)
        {
            var proveedor = new Proveedor
            {
                Nombre = dto.Nombre,
                Telefono = dto.Telefono,
                Activo = dto.Activo
            };

            await _repo.AgregarAsync(proveedor);

            return Ok(proveedor);
        }
    }
}
