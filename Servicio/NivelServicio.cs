using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.EF;

namespace Servicio
{

    public interface INivelSerivicio
    {
        Nivel ObtenerNivel(int id);
    }

    public class NivelServicio : INivelSerivicio
    {

        private readonly CasinoCrusadersContext _context;

        public NivelServicio(CasinoCrusadersContext context)
        {
            _context = context;
        }

        public Nivel ObtenerNivel(int id)
        {
            return _context.Nivels.Find(id);
        }
    }
}
