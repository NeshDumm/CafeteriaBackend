using CafeteriaBackend.Models;
using CafeteriaBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeteriaBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepositorio _repo;

        public UsuarioController(UsuarioRepositorio repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Usuario")]
        public async Task<ActionResult<List<Usuario>>> ObtenerTodos()
        {
            var usuarios = await _repo.ObtenerTodosAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Usuario")]
        public async Task<ActionResult<Usuario>> ObtenerPorId(int id)
        {
            var usuario = await _repo.BuscarPorIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Agregar([FromBody] Usuario dto)
        {
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                RolId = dto.RolId
            };
            await _repo.AgregarAsync(usuario);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] Usuario dto)
        {
            var usuario = new Usuario
            {
                Id = id,
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                RolId = dto.RolId
            };
            bool actualizado = await _repo.ActualizarAsync(usuario);
            if (!actualizado)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Eliminar(int id)
        {
            bool eliminado = await _repo.EliminarPorIdAsync(id);
            if (!eliminado)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}