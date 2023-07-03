using SC_701_1_ESTUDIO_DE_CASO_GRUPO_4.DTOs.List;
using System.ComponentModel.DataAnnotations;

namespace SC_701_1_ESTUDIO_DE_CASO_GRUPO_4.DTOs.Create
{
    public class InputTarea
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        [StringLength(50, MinimumLength = 5)]
        public string Asunto { get; set; }

        [Required]
        public bool Completado { get; set; }

        [Required]
        public double Esfuerzo { get; set; }

        public List<UserDTO> Users { get; set; }
    }
}
