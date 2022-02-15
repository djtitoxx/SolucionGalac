using ProductosAPI.DbContexts;
using ProductosAPI.Models;
using ProductosAPI.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ProductosAPI.Services
{
    public class ProductoService : IProductoService
    {
        //Inyeccion de Dependencia
        private readonly ProductosDbContext _dbContext;
        public ProductoService(ProductosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Producto> CreateProductoAsync(Producto producto)
        {
            try
            {
                
                await _dbContext.Productos.AddAsync(producto);
                await _dbContext.SaveChangesAsync();

               OperationProducto operacion = new OperationProducto();
                operacion.ProductoId = producto.Id;
                operacion.Precio = producto.Precio;
                operacion.Fecha = System.DateTime.Now;
                operacion.Cantidad = producto.Existencias;
                operacion.usuario = "Admin";
                await _dbContext.OperationProducto.AddAsync(operacion);

                await _dbContext.SaveChangesAsync();

                //Registro para Log
                await LogProducto(producto, "Crear Producto");

                return producto;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

      

        public async Task<List<Producto>> GetProductosAsync()
        {
            try
            {
                var productos = await _dbContext.Productos.ToListAsync();
                return productos;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<Producto> GetProductoByIdAsync(int productoId)
        {
            try
            {
                var producto = await _dbContext.Productos.Where(p => p.Id == productoId).FirstOrDefaultAsync();
                return producto;
            }
            catch (System.Exception)
            {

                throw;
            }
        }


        public async Task<Producto> UpdateProductoAsync(Producto producto)
        {
            try
            {
                if(producto.Id > 0)
                {
                    _dbContext.Entry(producto).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();

                    
                    //Registro para Log
                    await LogProducto(producto, "Modificar Producto");


                    return producto;
                }
                throw new System.Exception("Problema al consultar el producto");
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task DeleteProductoAsync(int productoId)
        {
            try
            {
                var producto = await GetProductoByIdAsync(productoId);
                if(producto != null)
                {
                    //Registro para Log
                    await LogProducto(producto, "Eliminar Producto");

                    _dbContext.Productos.Remove(producto);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<OperationProducto> OperationAddProducto(OperationProducto operacionProducto)
        {

            operacionProducto.usuario = "Admin";
            await _dbContext.OperationProducto.AddAsync(operacionProducto);
            await _dbContext.SaveChangesAsync();
                        
            var producto = await GetProductoByIdAsync(operacionProducto.ProductoId);
            producto.Existencias += operacionProducto.Cantidad;
            producto.Precio = operacionProducto.Precio;

            _dbContext.Entry(producto).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return operacionProducto;
        }

        public async Task<OperationProducto> OperationSubtractProducto(OperationProducto operacionProducto)
        {
            var producto = await GetProductoByIdAsync(operacionProducto.ProductoId);

            if (operacionProducto.Cantidad > producto.Existencias)
            {
                operacionProducto.usuario = "error";
                return operacionProducto;
            }

            operacionProducto.usuario = "Admin";
            operacionProducto.Cantidad = operacionProducto.Cantidad * -1;
            await _dbContext.OperationProducto.AddAsync(operacionProducto);
            await _dbContext.SaveChangesAsync();

            
            producto.Existencias += operacionProducto.Cantidad;
            _dbContext.Entry(producto).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return operacionProducto;
        }

        public async Task<LogProducto> LogProducto(Producto producto, string metodo)
        {
            //Registro para Log
            LogProducto logProducto = new LogProducto();
            logProducto.ProductoId = producto.Id;
            logProducto.Descripcion = producto.Descripcion;
            logProducto.Precio = producto.Precio;
            logProducto.Existencias = producto.Existencias;
            logProducto.Metodo = metodo;
            logProducto.Fecha = System.DateTime.Now;
            logProducto.usuario = "Admin";

            await _dbContext.LogProductos.AddAsync(logProducto);
            await _dbContext.SaveChangesAsync();

            return logProducto;
        }
    }

        
}

