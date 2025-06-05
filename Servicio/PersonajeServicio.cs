using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.EF;

namespace Servicio
{
    public interface IPersonajeServicio
    {
        Personaje ObtenerPersonaje(int? id);
    }
    public class PersonajeServicio : IPersonajeServicio
    {

        private readonly CasinoCrusadersContext _context;

        public PersonajeServicio(CasinoCrusadersContext crusadersContext)
        {
            _context = crusadersContext;
        }

        public Personaje ObtenerPersonaje(int? id)
        {
            return _context.Personajes.Find(id);
        }
    }
}
