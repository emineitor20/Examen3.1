using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tiendaweb.Models
{
    public class cervezas
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Ingresa el nombre de la cerveza")]
        [Display(Name = "Nombre Cerveza")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Ingresa el % de Alcohol")]
        [Display(Name = " % de Alcohol")]
        public double alcohol { get; set; }
        [Display(Name = "Estilo")]
        public int idEstilo { get; set; }
        [ForeignKey("idEstilo")]
        public estilo? estilo { get; set; }
        [Required(ErrorMessage = "Ingresa el precio de la cerveza")]
        [Display(Name = "Precio")]
        public double precio { get; set; }
        [Display (Name = "imagen")]
        public string? Urlimagen { get; set; }
    }
}
