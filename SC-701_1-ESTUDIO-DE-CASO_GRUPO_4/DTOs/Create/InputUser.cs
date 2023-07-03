using System.ComponentModel.DataAnnotations;

namespace SC_701_1_ESTUDIO_DE_CASO_GRUPO_4.DTOs.Create
{
    public class InputUser
    {
        public int id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
