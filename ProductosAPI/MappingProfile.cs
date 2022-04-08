using AutoMapper;
using ProductosAPI.DTO;
using ProductosAPI.Models;

namespace ProductosAPI
 
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {

            //// Producto - Destino
            ///  OperationProducto - Source
            /*
                CreateMap<OperationProducto, Producto>()
                .ForMember(destino=> destino.Id, origen => origen.MapFrom(source => source.ProductoId))
                .ForMember(d => d.Precio, o => o.MapFrom(s => s.Precio))
                .ForMember(d => d.Existencias, o => o.MapFrom(s => s.Cantidad));   
            */

            CreateMap<Producto, ProductoDTO>();//GET
            CreateMap<ProductoDTO, Producto>();//POST PUT

            CreateMap<OperationProducto, AddProductoDTO>();//GET
            CreateMap<AddProductoDTO, OperationProducto>();//POST PUT

            CreateMap<OperationProducto, SubstractProductoDTO>();//GET
            CreateMap<SubstractProductoDTO, OperationProducto>();//POST PUT

        }
    }
}
