using Entidades.EF;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CasinoCrusaders.ViewModels
{
    public class EnemigosViewModel
    {
        [ValidateNever]
        public List<Enemigo> Enemigos { get; set; }
        public Enemigo NuevoEnemigo { get; set; }
    }
}
