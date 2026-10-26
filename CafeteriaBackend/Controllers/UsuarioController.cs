using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CafeteriaBackend.Repositories;
using CafeteriaBackend.Models;
namespace CafeteriaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly UsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        public ICollection<Usuario> GetUsuarios()
        {
            var usuarios = _usuarioRepositorio.ObtenerUsuarios();
            return usuarios;
        }


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
