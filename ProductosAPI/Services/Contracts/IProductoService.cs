using ProductosAPI.DTO;
using ProductosAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductosAPI.Services.Contracts
{
    public interface IProductoService
    {    
        Task<List<Producto>> GetProductosAsync();
        Task<List<ProductoDTO>> GetApiProductosAsync();
        Task<Producto> GetProductoByIdAsync(int productoId);
        Task<ProductoDTO> CreateProductoAsync(ProductoDTO producto);        
        Task<Producto> UpdateProductoAsync(Producto producto);    
        Task DeleteProductoAsync(int productoId);

        //Operaciones Agregar, Sustraer y actualizacion Masiva Productos
        Task<AddProductoDTO> OperationAddProducto(AddProductoDTO operacionProducto);
        Task<SubstractProductoDTO> OperationSubtractProducto(SubstractProductoDTO operacionProducto);
        Task<bool> UpdatePrecioProductoAsync(float porcentaje);
                

        //Log de Productos
        Task<LogProducto> LogProducto(Producto producto, string metodo);
    }
}
