
using ProyectoFinalAPI_Antozzi.Entities;
using ProyectoFinalAPI_Antozzi.Repository.Interfaces;
using ProyectoFinalAPI_Antozzi.Repository.SQLServer;
namespace ProyectoFinalAPI_Antozzi.Services
{
    public class ProductoVendidoServices
    {
        protected readonly IProductoVendido _productoVendido;
        protected static ProductoVendidoServices _instance;
        private ProductoVendidoServices()
        {
            _productoVendido = new ProductoVendidoModel();
        }


        public static ProductoVendidoServices Instance() {
            if (_instance == null) { 
               _instance = new ProductoVendidoServices();
            }
          return _instance;
        }
        public List<Producto> GetByUserName(string userName)
        {
            return _productoVendido.GetByUserName(userName);
        }
        public ProductoVendido Add(ProductoVendido entity)
        {
            return _productoVendido.Add(entity);
        }

        public bool Delete(Int64 id)
        {
            return _productoVendido.Delete(id);

        }
        public ProductoVendido Get(Int64 id)
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
        public bool Update(ProductoVendido entity, Int64 id)
        {
            return _productoVendido.Update(entity, id);
        }

    }
}
