using System.ComponentModel.DataAnnotations;
namespace ProductosAPI.DTO

{
    public class ProductoDTO
    {

        
        [MaxLength(100)]
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public float Precio { get; set; }
        public int Existencias { get; set; }


    }
}
