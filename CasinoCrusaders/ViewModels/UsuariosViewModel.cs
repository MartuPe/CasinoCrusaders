using Entidades.EF;

namespace CasinoCrusaders.ViewModels
{
    public class UsuariosViewModel
    {
        public List<Usuario> Usuarios { get; set; }

        public EditarUsuarioViewModel Usuario { get; set; }

    }
}
