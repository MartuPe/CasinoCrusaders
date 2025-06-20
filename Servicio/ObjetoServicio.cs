using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.EF;

namespace Servicio
{

    public interface IObjetoServicio
    {
        Objeto ObtenerObjeto(int id);
        List<Objeto> ObtenerListaDeObjetos();


        void EliminarObjeto(int id);

        void EditarObjeto(Objeto objeto);
    }

    public class ObjetoServicio : IObjetoServicio
    {

        private readonly CasinoCrusadersContext _context;

        public ObjetoServicio(CasinoCrusadersContext context)
        {
            _context = context;
        }



        public Objeto ObtenerObjeto(int id)
        {
            Objeto? objeto = _context.Objetos.Find(id);
            return objeto;
        }

        public List<Objeto> ObtenerListaDeObjetos()
        {
          
          return _context.Objetos.ToList();
            
        }

        public void RegistrarObjeto(Objeto objeto)
        {
            _context.Objetos.Add(objeto);
            _context.SaveChanges();
        }

        public void EditarObjeto(Objeto objeto)
        {
            _context.Objetos.Update(objeto);
            _context.SaveChanges();
        }

        public void EliminarObjeto(int id)
        {
            var objeto = ObtenerObjeto(id);
            _context.Objetos.Remove(objeto);
            _context.SaveChanges();
        }


    }
}
