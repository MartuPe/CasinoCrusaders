using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.EF;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Servicio
{
    public interface IProgresoServicio
    {
        Progreso ObtenerProgreso(int id);
        void ActualizarProgreso(int id,int nivel,DateTime fecha);
    }

    public class ProgresoServicio : IProgresoServicio
    {

        private readonly CasinoCrusadersContext _context;

        public ProgresoServicio(CasinoCrusadersContext context)
        {
            _context = context;
        }

        public void ActualizarProgreso(int id, int nivel, DateTime fecha)
        {
            var progreso = ObtenerProgreso(id);

              if (progreso != null)
              {
                  progreso.FechaCreacion = fecha;
                  progreso.IdNivel = nivel;
                  _context.SaveChanges();
              }

        }

        

        public Progreso ObtenerProgreso(int id)
        {
            var personaje = _context.Personajes.Find(id);
            if(personaje != null)
            {
               return _context.Progresos.Where(p => p.IdPersonaje == id).First();
            }

            return null;
          

        }
    }
}
