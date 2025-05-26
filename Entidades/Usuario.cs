using System.ComponentModel.DataAnnotations;

namespace Entidades;

public class Usuario
{

    [Required(ErrorMessage = "El Nombre es requerido.")]
    public string Nombre_usuario { get; set; }

    [Required(ErrorMessage = "El Mail es requerido.")]
    [EmailAddress(ErrorMessage = "No es un correo electrónico válido.")]
    public string Mail { get; set; }

    [Required(ErrorMessage = "El Contraseña es requerido.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,20}$", ErrorMessage = "La contraseña debe tener entre 8 y 20 caracteres, al menos una mayúscula, una minúscula y un número.")]
    public string Contraseña { get; set; }

    [Required(ErrorMessage = "El tipo de usuario es requerido.")]
    public String Tipo_usuario { get; set; }

}
