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
        bool ActualizarPersonaje(Personaje personaje);
    }
    public class PersonajeServicio : IPersonajeServicio
    {

        private readonly CasinoCrusadersContext _context;

        public PersonajeServicio(CasinoCrusadersContext crusadersContext)
        {
            _context = crusadersContext;
        }

        public bool ActualizarPersonaje(Personaje personaje)
        {
            var personajeExistente = _context.Personajes.FirstOrDefault(p => p.IdPersonaje == personaje.IdPersonaje);

            if (personajeExistente == null)
                return false;

            personajeExistente.VidaMaxima = personaje.VidaMaxima;
            personajeExistente.VidaActual = personaje.VidaActual;
            personajeExistente.DañoAtaque = personaje.DañoAtaque;
            personajeExistente.Defensa = personaje.Defensa;
            personajeExistente.Monedas = personaje.Monedas;

            _context.SaveChanges();
            return true;
        }

        public Personaje ObtenerPersonaje(int? id)
        {
            return _context.Personajes.Find(id);
        }


    }
}
