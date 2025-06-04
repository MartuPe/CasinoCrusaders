using System;
using System.Collections.Generic;

namespace Entidades.EF;

public partial class Personaje
{
    public int IdPersonaje { get; set; }

    public int? VidaMaxima { get; set; }

    public int? VidaActual { get; set; }

    public int? DañoAtaque { get; set; }

    public int? Defensa { get; set; }

    public virtual ICollection<Progreso> Progresos { get; set; } = new List<Progreso>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
