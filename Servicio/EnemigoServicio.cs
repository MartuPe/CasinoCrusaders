using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.EF;

namespace Servicio
{

    public interface IEnemigoServicio
    {
        Enemigo ObtenerEnemigo(int id);
        List<Enemigo> ObtenerListaDeEnemigos();

        void RegistrarEnemigo(Enemigo enemigo);

        void EliminarEnemigo(int id);

        void EditarEnemigo(Enemigo enemigo);
    }

    public class EnemigoServicio : IEnemigoServicio
    {

        private readonly CasinoCrusadersContext _context;

        public EnemigoServicio(CasinoCrusadersContext context)
        {
            _context = context;
        }



        public Enemigo ObtenerEnemigo(int id)
        {
           Enemigo? enemigo = _context.Enemigos.Find(id);
            return enemigo;
        }

        public List<Enemigo> ObtenerListaDeEnemigos()
        {
          
          return _context.Enemigos.ToList();
            
        }

        public void RegistrarEnemigo(Enemigo enemigo)
        {
            _context.Enemigos.Add(enemigo);
            _context.SaveChanges();
        }

        public void EditarEnemigo(Enemigo enemigo)
        {
            _context.Enemigos.Update(enemigo);
            _context.SaveChanges();
        }

        public void EliminarEnemigo(int id)
        {
            var enemigo = ObtenerEnemigo(id);
            _context.Enemigos.Remove(enemigo);
            _context.SaveChanges();
        }


    }
}
