using System;
using System.Collections.Generic;

namespace Entidades.EF;

public partial class Objeto
{
    public int IdObjeto { get; set; }

    public string Nombre { get; set; } = null!;

    public int Estadistica { get; set; }

    public int Precio { get; set; }
}
