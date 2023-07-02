using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caso1.Domain.Entities
{
    public class Tarea
    {
        public Tarea()
        {
            Usuarios = new List<Usuario>();
        }

        [Key]
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        [ForeignKey("Usuario")]
        public int Usuario { get; set; }

        [StringLength(50, MinimumLength = 5)]
        public string Asunto { get; set; }

        [Required]
        public bool Completado { get; set; }

        [Required]
        public double Esfuerzo { get; set; }

        public List<Usuario> Usuarios { get; set; }
    }
}
