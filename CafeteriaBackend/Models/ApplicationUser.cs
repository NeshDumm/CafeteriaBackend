using Microsoft.AspNetCore.Identity;

namespace CafeteriaBackend.Models;

public class ApplicationUser : IdentityUser
{
    public string NombreCompleto { get; set; } = string.Empty;
}