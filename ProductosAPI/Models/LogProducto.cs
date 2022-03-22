using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductosAPI.Models
{
    public class LogProducto
    {
        [Key]
        public int Id { get; set; }

        public int ProductoId { get; set; }
        [ForeignKey("ProductoId")]

        [MaxLength(100)]
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public float Precio { get; set; }
        public int Existencias { get; set; }

        [MaxLength(100)]
        public string Metodo { get; set; }

        public DateTime Fecha { get; set; }

        [JsonIgnore]
        [MaxLength(20)]
        public string usuario { get; set; }
    }
}
