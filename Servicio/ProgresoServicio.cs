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

    void ActualizarProgreso(int id, int nivel);

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
        var topUsuarios = _context.Progresos
          .GroupBy(p => p.IdPersonaje)
          .Select(g => new
          {
              IdPersonaje = g.Key,
              MaxNivel = g.Max(p => p.IdNivel)
          })
          .OrderByDescending(p => p.MaxNivel)
          .Take(5)
          .ToList();


        var usuariosOrdenados = topUsuarios
            .Select(p =>
                _context.Usuarios.FirstOrDefault(u => u.IdPersonaje == p.IdPersonaje)
            )
            .Where(u => u != null)
            .ToList();

        return usuariosOrdenados;


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
        if (personaje != null)
        {
            return _context.Progresos.Where(p => p.IdPersonaje == id).First();
        }

        return null;


    }


}
