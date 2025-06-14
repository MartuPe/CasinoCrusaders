using Entidades.EF;
using System.ComponentModel.DataAnnotations;

namespace CasinoCrusaders.ViewModels
{
    public class EditarUsuarioViewModel
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage ="el nombre es obligatorio")]
        public string NombreUsuario { get; set; } = null!;

        [Required(ErrorMessage = "El Mail es requerido.")]
        [EmailAddress(ErrorMessage = "No es un correo electrónico válido.")]
        public string Gmail { get; set; } = null!;
    }
}
