
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProductosAPI.Models
{
    public class OperationProducto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductoId { get; set; }
                
        [JsonIgnore]
        public Producto Producto { get; set; }

        [Required]
        public float Precio { get; set; }

        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }

        [JsonIgnore]
        [MaxLength(20)]
        public string usuario { get; set; }

    }
}
