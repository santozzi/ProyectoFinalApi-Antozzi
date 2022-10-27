
using ProyectoFinalAPI_Antozzi.Entities;
using ProyectoFinalAPI_Antozzi.Repository.Interfaces;
using ProyectoFinalAPI_Antozzi.Repository.SQLServer;

namespace ProyectoFinalAPI_Antozzi.Services
{
    public class ProductoServices 
    {
        protected readonly IProductoModel _productoModel;
        public ProductoServices() {
            _productoModel = new ProductoModel();
        }
        public Producto Add(Producto entity)
        {
            return _productoModel.Add(entity);
        }

        public bool Delete(int id)
        {
            return this._productoModel.Delete(id);
        }

        public Producto Get(int id)
        {
            return _productoModel.Get(id);
        }

        public List<Producto> GetAll()
        {
            return _productoModel.GetAll();

        }

        public List<Producto> GetByIdUsuario(int id)
        {
            return _productoModel.GetByIdUsuario(id);
        }

        public bool Update(Producto entity,int id)
        {
            return _productoModel.Update(entity,id);
        }

        
    }
}
