using Microsoft.AspNetCore.Mvc;
using ProyectoFinalAPI_Antozzi.Entities;
using ProyectoFinalAPI_Antozzi.Repository.Interfaces;
using ProyectoFinalAPI_Antozzi.Repository.SQLServer;

namespace ProyectoFinalAPI_Antozzi.Services
{
    public class VentaServices
    {
        protected readonly IVentaModel _ventaModel;
        public VentaServices() {
            _ventaModel = new VentaModel();
        }

        public List<Venta> GetVentasByIdUsuer(int idUsuario) {
            return _ventaModel.GetVentasByIdUsuer(idUsuario);
        }

        public Venta Crear(Venta venta) {
            return _ventaModel.Add(venta);
        }
        public bool Delete(int id) {
            return _ventaModel.Delete(id) ;
        }
public bool DeleteByIdUser(int id){
          return _ventaModel.DeleteByIdUser(id)  ;
        }
public Venta Get(int id){
          return _ventaModel.Get(id)  ;
        }
public List<Venta> GetAll(){
          return _ventaModel.GetAll()  ;
        }
public bool Update(Venta entity,int id){
          return _ventaModel.Update(entity,id)  ;
        }

    }
}
