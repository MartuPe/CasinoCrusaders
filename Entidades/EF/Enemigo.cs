using System;
using System.Collections.Generic;

namespace Entidades.EF;

public partial class Enemigo
{
    public int IdEnemigo { get; set; }

    public string? Nombre { get; set; }

    public int? Vida { get; set; }

    public int? Ataque { get; set; }

    public int? Defensa { get; set; }

    public string? Descripcion { get; set; }

    public string? Imagen { get; set; }

    public virtual ICollection<Nivel> Nivels { get; set; } = new List<Nivel>();
}
