using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Entidades.EF
{
    [ModelMetadataType(typeof(EnemigoMetadata))]

    public partial class Enemigo
    {
    }

    public class EnemigoMetadata
    {

        [Required (ErrorMessage ="el campo nombre es obligatorio")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage ="la descripcion es obligatoria")]
        public string? Descripcion { get; set; }


        [Required(ErrorMessage = "el campo vida es obligatorio")]
        [Range (1,int.MaxValue, ErrorMessage ="el campo vida debe ser mayor a 0")]
        public int? Vida { get; set; }
        [Required(ErrorMessage = "el campo ataque es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "el campo ataque debe ser mayor a 0")]
        public int? Ataque { get; set; }

        [Required(ErrorMessage = "el campo defensa es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "el campo defensa debe ser mayor a 0")]
        public int? Defensa { get; set; }
    }
}
