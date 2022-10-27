
using ProyectoFinalAPI_Antozzi.Entities;
using ProyectoFinalAPI_Antozzi.Repository.Interfaces;
using ProyectoFinalAPI_Antozzi.Repository.SQLServer;
namespace ProyectoFinalAPI_Antozzi.Services
{
    public class ProductoVendidoServices
    {
        protected readonly IProductoVendido _productoVendido;
        public ProductoVendidoServices()
        {
            _productoVendido = new ProductoVendidoModel();
        }

        public List<Producto> GetByUserName(string userName)
        {
            return _productoVendido.GetByUserName(userName);
        }
        public ProductoVendido Add(ProductoVendido entity)
        {
            return _productoVendido.Add(entity);
        }

        public bool Delete(int id)
        {
            return _productoVendido.Delete(id);

        }
        public ProductoVendido Get(int id)
        {
            return _productoVendido.Get(id);
        }
        public List<ProductoVendido> GetAll()
        {
            return _productoVendido.GetAll();
        }
        public List<Producto> GetByIdUsuario(Int64 id)
        {
            return _productoVendido.GetByIdUsuario(id);
        }
        public bool Update(ProductoVendido entity, int id)
        {
            return _productoVendido.Update(entity, id);
        }

    }
}
