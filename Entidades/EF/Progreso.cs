using System;
using System.Collections.Generic;

namespace Entidades.EF;

public partial class Progreso
{
    public int IdProgreso { get; set; }

    public int IdNivel { get; set; }

    public int IdPersonaje { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual Nivel IdNivelNavigation { get; set; } = null!;

    public virtual Personaje IdPersonajeNavigation { get; set; } = null!;
}
