using ProductosAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductosAPI.Services.Contracts
{
    public interface IProductoService
    {    
        Task<List<Producto>> GetProductosAsync();
        Task<Producto> GetProductoByIdAsync(int productoId);
        Task<Producto> CreateProductoAsync(Producto producto);        
        Task<Producto> UpdateProductoAsync(Producto producto);    
        Task DeleteProductoAsync(int productoId);

        //Operaciones Agregar, Sustraer y actualizacion Masiva Productos
        Task<OperationProducto> OperationAddProducto(OperationProducto operacionProducto);
        Task<OperationProducto> OperationSubtractProducto(OperationProducto operacionProducto);

        Task<bool> UpdatePrecioProductoAsync(float porcentaje);

        

        //Log de Productos
        Task<LogProducto> LogProducto(Producto producto, string metodo);
    }
}
