using Entidades;
using Entidades.EF;
using Microsoft.AspNetCore.Identity;

namespace Servicio;

public interface IUsuarioServicio
{
    void AgregarUsuario(Usuario usuario);
    Usuario ObtenerUsuarioPorId(int id);
    List<Usuario> ObtenerTodosLosUsuarios();
    void ActualizarUsuario(Usuario usuario);
    void EliminarUsuario(int id);
}
public class UsuarioServicio : IUsuarioServicio
{
    CasinoCrusadersContext _context;
    private readonly PasswordHasher<string> _passwordHasher;


    public UsuarioServicio(CasinoCrusadersContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<string>();
    }
    public void AgregarUsuario(Usuario usuario)
    {
        string token = Guid.NewGuid().ToString("N");
        DateTime expiration = DateTime.UtcNow.AddHours(24);
        string passwordHasheada = _passwordHasher.HashPassword(usuario.NombreUsuario, usuario.Contraseña);


        usuario.EmailVerificacionToken = token;
        usuario.ExpiracionToken = expiration;


        _context.Usuarios.Add(usuario);
        _context.SaveChanges();

    }

    public Usuario ObtenerUsuarioPorId(int id)
    {
        // Lógica para obtener un usuario por su ID
        // Esto podría incluir la búsqueda en una base de datos
        return new Usuario(); // Retorna un nuevo objeto Usuario como ejemplo
    }

    public List<Usuario> ObtenerTodosLosUsuarios()
    {
        // Lógica para obtener todos los usuarios
        // Esto podría incluir la búsqueda en una base de datos
        return new List<Usuario>(); // Retorna una lista vacía como ejemplo
    }

    public void ActualizarUsuario(Usuario usuario)
    {
        // Lógica para actualizar un usuario existente
        // Esto podría incluir la validación de datos y la actualización en una base de datos
    }

    public void EliminarUsuario(int id)
    {
        // Lógica para eliminar un usuario por su ID
        // Esto podría incluir la eliminación en una base de datos
    }


}
