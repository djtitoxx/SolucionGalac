using System.ComponentModel.DataAnnotations;

namespace ProductosAPI.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public float Precio { get; set; }
        public int Existencias { get; set; }

        


    }
}
