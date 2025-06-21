using Entidades.EF;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Servicio;

public interface IProgresoServicio
{
    List<Usuario> ObtenerLos5UsuariosConMayorProgreso();

    Enemigo ObtenerEnemigoMasDificil();

    Enemigo ObtenerEnemigoMasFacil();

    Progreso ObtenerProgreso(int id);
  
    void ActualizarProgreso(int id,int nivel);

}
public class ProgresoServicio : IProgresoServicio
{
    private readonly CasinoCrusadersContext _context;
    public ProgresoServicio(CasinoCrusadersContext context)
    {
        _context = context;
    }
    public List<Usuario> ObtenerLos5UsuariosConMayorProgreso()
    {        
            var usuariosConProgreso = _context.Progresos
                .GroupBy(p => p.IdPersonaje)
                .Select(g => new
                {
                    IdPersonaje = g.Key,
                    MaxNivel = g.Max(p => p.IdNivel)
                })
                .OrderByDescending(p => p.MaxNivel)
                .Take(5)
                .ToList();
            var idsPersonajes = usuariosConProgreso.Select(u => u.IdPersonaje).ToList();
            return _context.Usuarios
                .Where(u => idsPersonajes.Contains(u.IdPersonaje.Value))
                .ToList();

    }
    public Enemigo ObtenerEnemigoMasDificil()
    {
        var enemigoMasDificil = _context.Enemigos
            .OrderByDescending(e => e.Vida + e.Ataque + e.Defensa)
            .FirstOrDefault();
        return enemigoMasDificil;
    }

    public Enemigo ObtenerEnemigoMasFacil()
    {
        var enemigoMasFacil = _context.Enemigos
            .OrderBy(e => e.Vida + e.Ataque + e.Defensa)
            .FirstOrDefault();
        return enemigoMasFacil;
    }

   public void ActualizarProgreso(int id, int nivel)
   {
       var progreso = ObtenerProgreso(id);

         if (progreso != null)
         {
            progreso.FechaCreacion = DateTime.Now;
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

    //public Usuario ObtenerUsuarioGanador()
    //{
    //    var usuarioGanador = _context.Progresos
    //        .GroupBy(p => p.IdPersonaje)
    //        .Select(g => new
    //        {
    //            IdPersonaje = g.Key,
    //            MaxNivel = g.Max(p => p.IdNivel)
    //        })
    //        .OrderByDescending(p => p.MaxNivel)
    //        .FirstOrDefault();
    //    if (usuarioGanador != null)
    //    {
    //        return _context.Usuarios.FirstOrDefault(u => u.IdPersonaje == usuarioGanador.IdPersonaje);
    //    }
    //    return null;
    //}


    //public int ObtenerProgresoDe1Usuario(int IdPersonaje)
    //{
    //    var personaje = _context.Personajes.FirstOrDefault(p => p.IdPersonaje == IdPersonaje);
    //    int cantidadDeNiveles = ObtenerCantidadDeNiveles();

    //    if (personaje != null)
    //    {
    //        var personajeEnProgreso = _context.Progresos.FirstOrDefault(p => p.IdPersonaje == IdPersonaje);
    //        int nivelPersonaje = personajeEnProgreso?.IdNivel ?? 0;
    //        if (nivelPersonaje == cantidadDeNiveles)
    //        {
    //            return 100;
    //        }
    //        else
    //        {
    //            return (nivelPersonaje * 100) / cantidadDeNiveles;
    //        }
    //    }
    //    return 0;
    //}


    //private int ObtenerCantidadDeNiveles()
    //{
    //    return _context.Nivels.Count();
    //}

}
