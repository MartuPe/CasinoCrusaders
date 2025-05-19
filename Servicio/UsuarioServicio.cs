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

}
