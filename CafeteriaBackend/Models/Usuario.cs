namespace CafeteriaBackend.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }

        // este mejor que se herede luego jeje 
        // public string Contrasena { get; set; } 
        public string Telefono { get; set; }
        public int RolId { get; set; }
    }
}
