using System;
using System.Collections.Generic;

namespace Entidades.EF;

public partial class Nivel
{
    public int IdNivel { get; set; }

    public int? IdEnemigo { get; set; }

    public string? Nombre { get; set; }

    public int IdTipoCasillero { get; set; }

    public virtual Enemigo? IdEnemigoNavigation { get; set; }

    public virtual TipoCasillero IdTipoCasilleroNavigation { get; set; } = null!;

    public virtual ICollection<Progreso> Progresos { get; set; } = new List<Progreso>();
}
