using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductosAPI.Models;
using ProductosAPI.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        //Inyeccion de dependencias!
        private readonly IProductoService _productoService;
        public ProductoController(IProductoService productoService)
        {            
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> GetProductosAsync()
        {
            try
            {
                var productos = await _productoService.GetProductosAsync();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{productoId:int}", Name = "GetProductoId")]
        public async Task<ActionResult<Producto>> GetProductoByIdAsync(int productoId)
        {
            try
            {
                var producto = await _productoService.GetProductoByIdAsync(productoId);
                if(producto != null) return Ok(producto);
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> CreateProductoAsync(Producto producto)
        {
            try
            {
                var savedProducto = await _productoService.CreateProductoAsync(producto);

                return CreatedAtRoute("GetProductoId", new { productoId  = savedProducto.Id}, savedProducto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddProducto")]
        public async Task<ActionResult<OperationProducto>> OperationAddProducto(OperationProducto operationProducto)
        {
            try
            {
                var savedProducto = await _productoService.OperationAddProducto(operationProducto);

                return CreatedAtRoute("GetProductoId", new { productoId = savedProducto.ProductoId }, savedProducto.Producto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SubtractProducto")]
        public async Task<ActionResult<OperationProducto>> OperationSubtractProducto(OperationProducto operationProducto)
        {
            try
            {
                var savedProducto = await _productoService.OperationSubtractProducto(operationProducto);

                if (savedProducto.usuario == "error" )
                {
                    return BadRequest("No pueden quedar existencias en Negativo, por favor consulte de nuevo las existencias del producto a despachar! en /api/Producto/{productoId}");
                }

                return CreatedAtRoute("GetProductoId", new { productoId = savedProducto.ProductoId }, savedProducto.Producto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpDelete("{productoId:int}")]
        public async Task<IActionResult> DeleteProductoAsync(int productoId)
        {
            try
            {
                await _productoService.DeleteProductoAsync(productoId);
                return Ok("Producto eliminado en DB!, Nota: No se recomienda eliminar, se recomienda cambiar de estatus para ocultar cualquier informacion que ya no se desee ver!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Producto>> UpdateProductoAsync(Producto product)
        {
            try
            {
                var updateProducto = await _productoService.UpdateProductoAsync(product);
                return Ok(updateProducto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}
