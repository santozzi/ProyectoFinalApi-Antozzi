
using ProyectoFinalAPI_Antozzi.Entities;
using ProyectoFinalAPI_Antozzi.Repository.Interfaces;
using ProyectoFinalAPI_Antozzi.Repository.SQLServer;

namespace ProyectoFinalAPI_Antozzi.Services
{
    public class ProductoServices 
    {
        protected readonly IProductoModel _productoModel;
        protected static ProductoServices? _instance;

        private ProductoServices() {
            _productoModel = new ProductoModel();
        }

        public static ProductoServices Instance() {
            if (_instance == null) { 
                _instance = new ProductoServices();
            }
          return _instance;
        }
        public Producto Add(Producto entity)
        {
            return _productoModel.Add(entity);
        }

        public bool Delete(Int64 id)
        {
            return this._productoModel.Delete(id);
        }

        public Producto Get(Int64 id)
        {
            return _productoModel.Get(id);
        }

        public List<Producto> GetAll()
        {
            return _productoModel.GetAll();

        }

        public List<Producto> GetByIdUsuario(Int64 id)
        {
            return _productoModel.GetByIdUsuario(id);
        }

        public bool Update(Producto entity, Int64 id)
        {
            return _productoModel.Update(entity,id);
        }
        public void SubstractProductStock(Int64 idProducto, int quantity) { 
           _productoModel.SubstractProductStock(idProducto,quantity);
        }

    }
}
