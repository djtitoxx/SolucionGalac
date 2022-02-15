using Microsoft.EntityFrameworkCore;
using ProductosAPI.Models;

namespace ProductosAPI.DbContexts
{
    public class ProductosDbContext: DbContext
    {
        public DbSet<Producto> Productos { get; set; }

        public DbSet<OperationProducto> OperationProducto { get; set; }

        public DbSet<LogProducto> LogProductos { get; set; }

        public ProductosDbContext(DbContextOptions<ProductosDbContext> options) : base(options)
        {

        }
    }
}
