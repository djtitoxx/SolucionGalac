using ProductosAPI.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductosAPI.DTO
{
    public class AddProductoDTO
    {

        [Required]
        public int ProductoId { get; set; }

        [JsonIgnore]
        public Producto Producto { get; set; }

        [Required]
        public float Precio { get; set; }

        public int Cantidad { get; set; }

        [JsonIgnore]
        public DateTime Fecha { get; set; }

        [JsonIgnore]
        [MaxLength(20)]
        public string usuario { get; set; }

    }
}
