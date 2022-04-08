using ProductosAPI.DbContexts;
using ProductosAPI.Models;
using ProductosAPI.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;
using ProductosAPI.DTO;

namespace ProductosAPI.Services
{
    public class ProductoService : IProductoService
    {
        //Inyeccion de Dependencia
        private readonly ProductosDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProductoService(ProductosDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ProductoDTO> CreateProductoAsync(ProductoDTO producto)
        {
            try
            {
                Producto request_producto = _mapper.Map<Producto>(producto);
                await _dbContext.Productos.AddAsync(request_producto);
             await _dbContext.SaveChangesAsync();
                



                OperationProducto operacion = new OperationProducto();
                operacion.ProductoId = request_producto.Id;
                operacion.Precio = producto.Precio;
                operacion.Precio = request_producto.Precio;
                operacion.Fecha = System.DateTime.Now;
                operacion.Cantidad = producto.Existencias;
                operacion.usuario = "Admin";
                await _dbContext.OperationProducto.AddAsync(operacion);

                await _dbContext.SaveChangesAsync();

                //Registro para Log
               // await LogProducto(producto, "Crear Producto");

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

        public async Task<List<ProductoDTO>> GetApiProductosAsync()
        {
            try
            {
                var productos = await _dbContext.Productos.ToListAsync();
                var productosDTO = _mapper.Map<List<ProductoDTO>>(productos);
                return productosDTO;
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

        public async Task<AddProductoDTO> OperationAddProducto(AddProductoDTO operacionProducto)
        {
            OperationProducto request_operacion = _mapper.Map<OperationProducto>(operacionProducto);
            request_operacion.usuario = "Admin";
            request_operacion.Fecha = System.DateTime.Now;
            await _dbContext.OperationProducto.AddAsync(request_operacion);
            await _dbContext.SaveChangesAsync();
                        
            var producto = await GetProductoByIdAsync(request_operacion.ProductoId);
            producto.Existencias += request_operacion.Cantidad;
            producto.Precio = request_operacion.Precio;

            _dbContext.Entry(producto).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return operacionProducto;
        }

        public async Task<SubstractProductoDTO> OperationSubtractProducto(SubstractProductoDTO operacionProducto)
        {
            OperationProducto request_operacion = _mapper.Map<OperationProducto>(operacionProducto);
            var producto = await GetProductoByIdAsync(request_operacion.ProductoId);

            if (operacionProducto.Cantidad > producto.Existencias)
            {
                request_operacion.usuario = "error";
                request_operacion.Fecha = System.DateTime.Now;
                return operacionProducto;
            }

            request_operacion.usuario = "Admin";
            request_operacion.Fecha = System.DateTime.Now;
            request_operacion.Cantidad = request_operacion.Cantidad * -1;
            await _dbContext.OperationProducto.AddAsync(request_operacion);
            await _dbContext.SaveChangesAsync();

            
            producto.Existencias += request_operacion.Cantidad;
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

        public async Task<bool> UpdatePrecioProductoAsync(float porcentaje)
        {
            try
            {

                var todosProductos = await GetProductosAsync();
                var men = "";
                if(porcentaje > 0)
                {
                    var m1 = "Aumento";
                    men = m1;
                }
                else
                {
                    var m1 = "Reducción";
                    men = m1;
                }
                
                foreach(var producto in todosProductos)
                {
                   
                    float numero = porcentaje/100;
                    float descuento = producto.Precio * numero;
                    float precioActual = producto.Precio + descuento;
                    var message = men + " de Precio en " + porcentaje + " %" + "/ Precio Actual: " + precioActual;

                    //Registro para Log
                    await LogProducto(producto, message);

                    producto.Precio += descuento;

                   

                    _dbContext.Entry(producto).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();

                    

                }
                  
                    return true;
                
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        
    }

        
}

