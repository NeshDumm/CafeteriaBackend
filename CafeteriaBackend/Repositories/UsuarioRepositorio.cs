using CafeteriaBackend.Models;

namespace CafeteriaBackend.Repositories

{
    public class UsuarioRepositorio
    {
        private readonly List<Usuario> _usuarios;

        public UsuarioRepositorio()
        {
            _usuarios = new List<Usuario>();
            _usuarios.Add(new Usuario { Id = 1, Nombre = "Oscar Genial", Correo = "sapo@gmail.com" , Telefono = "1234567890", RolId = 1 });
            _usuarios.Add(new Usuario { Id = 2, Nombre = "Anaela la chida", Correo = "sapo2@gmail.com", Telefono = "0987654321", RolId = 2 });
            _usuarios.Add(new Usuario { Id = 3, Nombre = "Pepe", Correo = "sapo3@gmail.com", Telefono = "1122334455", RolId = 3 });
        }

        public List<Usuario> ObtenerUsuarios()
        {
            return _usuarios;
        }

        public void AgregarUsuario(Usuario usuario)
        {
            this._usuarios.Add(usuario);
        }

        public Usuario ObtenerUsuarioPorId(int id)
        {
            return _usuarios.FirstOrDefault(u => u.Id == id);
        }

        public bool EliminarUsuario(int id)
        {
            Usuario? usuario = _usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)
            {
                _usuarios.Remove(usuario);
                return true;
            }
            return false;
        }

        public Usuario EditarUsuario(int id, Usuario usuarioActualizado)
        {
            Usuario? usuario = _usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)
            {
                usuario.Nombre = usuarioActualizado.Nombre;
                usuario.Correo = usuarioActualizado.Correo;
                usuario.Telefono = usuarioActualizado.Telefono;
                usuario.RolId = usuarioActualizado.RolId;
            }
            return usuario;
        }
    }
}
