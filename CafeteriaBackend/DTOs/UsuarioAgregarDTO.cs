namespace CafeteriaBackend.DTOs
{
    public class UsuarioAgregarDTO //DTOs son para tener la información necesaria para agregar un usuario, no es necesario tener todos los campos de la clase Usuario
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public int RolId { get; set; }
    }
}
