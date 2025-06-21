using System;
using System.Collections.Generic;

namespace Entidades.EF;

public partial class TipoCasillero
{
    public int IdTipoCasillero { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Nivel> Nivels { get; set; } = new List<Nivel>();
}
