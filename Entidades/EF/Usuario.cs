using System;
using System.Collections.Generic;

namespace Entidades.EF;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Gmail { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string NombreUsuario { get; set; } = null!;

    public string? TipoUsuario { get; set; }

    public int? IdPersonaje { get; set; }

    public string? EmailVerificacionToken { get; set; }

    public DateTime? ExpiracionToken { get; set; }

    public bool EmailVerificado { get; set; }
}
