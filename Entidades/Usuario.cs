
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades.EF;

[ModelMetadataType(typeof(UsuarioMetadata))]

public partial class Usuario
{
    [NotMapped]
    public string RepiteContraseña {get; set; } = null!;
}

public class UsuarioMetadata
{
    [Required(ErrorMessage = "El Nombre es requerido.")]
    public string NombreUsuario { get; set; } = null!;

    [Required(ErrorMessage = "El Mail es requerido.")]
    [EmailAddress(ErrorMessage = "No es un correo electrónico válido.")]
    public string Gmail { get; set; } = null!;

    [Required(ErrorMessage = "La Contraseña es requerido.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,20}$", ErrorMessage = "La contraseña debe tener entre 8 y 20 caracteres, al menos una mayúscula, una minúscula y un número.")]
    public string Contraseña { get; set; } = null!;

    [Required(ErrorMessage = "Debes repetir la contraseña")]
    [Compare("Contraseña", ErrorMessage = "Las contraseñas no coinciden.")]
    public string RepiteContraseña { get; set; } = null!;

}