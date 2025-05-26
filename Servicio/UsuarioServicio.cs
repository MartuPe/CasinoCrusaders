using Entidades;

namespace Servicio;

public interface IUsuarioServicio
{
    void CrearUsuario(Usuario usuario);
    Usuario ObtenerUsuarioPorId(int id);
    List<Usuario> ObtenerTodosLosUsuarios();
    void ActualizarUsuario(Usuario usuario);
    void EliminarUsuario(int id);
}
public class UsuarioServicio
{
    public void CrearUsuario(Usuario usuario)
    {
        // Lógica para crear un nuevo usuario
        // Esto podría incluir la validación de datos y el almacenamiento en una base de datos
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
