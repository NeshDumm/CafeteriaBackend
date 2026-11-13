using CafeteriaBackend.Data;
using CafeteriaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeteriaBackend.Repositories;

public class UsuarioRepositorio
{
    private readonly CafeteriaDbContext _context;

    public UsuarioRepositorio(CafeteriaDbContext context)
    {
        _context = context;
    }

    public async Task<List<Usuario>> ObtenerTodosAsync()
    {
        return await _context.Usuarios.AsNoTracking().ToListAsync();
    }

    public async Task<Usuario?> BuscarPorIdAsync(int id)
    {
        return await _context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Usuario> AgregarAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<bool> ActualizarAsync(Usuario usuario)
    {
        var existente = await _context.Usuarios.FindAsync(usuario.Id);
        if (existente == null)
        {
            return false;
        }

        existente.Nombre = usuario.Nombre;
        existente.Correo = usuario.Correo;
        existente.Telefono = usuario.Telefono;
        existente.RolId = usuario.RolId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarPorIdAsync(int id)
    {
        var existente = await _context.Usuarios.FindAsync(id);
        if (existente == null)
        {
            return false;
        }

        _context.Usuarios.Remove(existente);
        await _context.SaveChangesAsync();
        return true;
    }
}