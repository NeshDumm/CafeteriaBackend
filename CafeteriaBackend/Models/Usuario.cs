namespace CafeteriaBackend.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;

        // este mejor que se herede luego jeje 
        // public string Contrasena { get; set; } 
        public string Telefono { get; set; } = string.Empty;
        public int RolId { get; set; }
    }
}