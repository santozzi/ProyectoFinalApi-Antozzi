
using ProyectoFinalAPI_Antozzi.Entities;
using ProyectoFinalAPI_Antozzi.Repository.Interfaces;
using ProyectoFinalAPI_Antozzi.Repository.SQLServer;

namespace ProyectoFinalAPI_Antozzi.Services
{
    public class VentaServices
    {
        protected readonly IVentaModel _ventaModel;
        protected static VentaServices? _instance;
        private VentaServices()
        {
            _ventaModel = new VentaModel();
        }

        public static VentaServices Instance()
        {
            if (_instance == null)
            {
                _instance = new VentaServices();

            }
            return _instance;

        }
        public List<Venta> GetVentasByIdUsuer(Int64 idUsuario)
        {
            return _ventaModel.GetVentasByIdUsuer(idUsuario);
        }

        public bool Crear(Venta venta, List<Producto> productosVendidos)
        {
            return _ventaModel.Add(venta, productosVendidos);
        }
        public bool Delete(Int64 id)
        {
            return _ventaModel.Delete(id);
        }
        public bool DeleteByIdUser(Int64 id)
        {
            return _ventaModel.DeleteByIdUser(id);
        }
        public Venta Get(Int64 id)
        {
            return _ventaModel.Get(id);
        }
        public List<Venta> GetAll()
        {
            return _ventaModel.GetAll();
        }
        public bool Update(Venta entity, Int64 id)
        {
            return _ventaModel.Update(entity, id);
        }

    }
}
