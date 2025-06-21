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

    public int Monedas { get; set; }

    public virtual Progreso? Progreso { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
