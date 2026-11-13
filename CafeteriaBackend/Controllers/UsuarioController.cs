using CafeteriaBackend.Models;
using CafeteriaBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace CafeteriaBackend.Controllers
{
    [Authorize] 
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {

        private readonly UsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        [Authorize(Roles = "Admin, Usuario")]
        [HttpGet]
        public ICollection<Usuario> GetUsuarios()
        {
            var usuarios = _usuarioRepositorio.ObtenerUsuarios();
            return usuarios;
        }

        [Authorize(Roles = "Admin, Usuario")]
        [HttpGet("{id}")]
        public ActionResult<Usuario> ObtenerPorId(int id)
        {
            var usuario = _usuarioRepositorio.ObtenerUsuarioPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult EliminarUsuario(int id)
        {
            bool eliminado = _usuarioRepositorio.EliminarUsuario(id);
            if (!eliminado)
            {
                return NotFound();
            }
            return Ok();
        }

        //AGREGAR
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Agregar([FromBody] Usuario usuario )
        {
            var usr = new Usuario
            {
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Telefono = usuario.Telefono,
                RolId = usuario.RolId
            };
            _usuarioRepositorio.AgregarUsuario(usr);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = usr.Id }, usr);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult EditarUsuario(int id, [FromBody] Usuario usuarioActualizado)
        {
            var usuario = _usuarioRepositorio.EditarUsuario(id, usuarioActualizado);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }
    }
}
