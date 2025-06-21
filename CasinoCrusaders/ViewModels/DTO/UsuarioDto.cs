namespace CasinoCrusaders.ViewModels.DTO
{
    public class UsuarioDto
    {

        public int IdUsuario { get; set; }

        public string Gmail { get; set; } = null!;

        public string NombreUsuario { get; set; } = null!;

        public string? TipoUsuario { get; set; }

        public int? IdPersonaje { get; set; }

    }
}
