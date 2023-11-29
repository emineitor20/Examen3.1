using System.ComponentModel.DataAnnotations;

namespace Tiendaweb.Models
{
    public class estilo
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Ingresa el nombre del estilo")]
        [Display(Name = "Nombre Estilo")]
        public string nombre { get; set; }
    }
}
