
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Entidades.EF;

[ModelMetadataType(typeof(UsuarioMetadata))]

public partial class Usuario
{
}

public class UsuarioMetadata
{
    [Required(ErrorMessage = "El Nombre es requerido.")]
    public string NombreUsuario { get; set; } = null!;

    [Required(ErrorMessage = "El Mail es requerido.")]
    [EmailAddress(ErrorMessage = "No es un correo electrónico válido.")]
    public string Gmail { get; set; } = null!;

    [Required(ErrorMessage = "El Contraseña es requerido.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,20}$", ErrorMessage = "La contraseña debe tener entre 8 y 20 caracteres, al menos una mayúscula, una minúscula y un número.")]
    public string Contraseña { get; set; } = null!;

    [Required(ErrorMessage = "El tipo de usuario es requerido.")]
    public string TipoUsuario { get; set; } = null!;
}