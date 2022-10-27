using Microsoft.AspNetCore.Mvc;
using ProyectoFinalAPI_Antozzi.Entities;
using ProyectoFinalAPI_Antozzi.Services;

namespace ProyectoFinalAPI_Antozzi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : ControllerBase
    {
        private readonly VentaServices _ventaServices;

        public VentaController()
        {
            _ventaServices = new VentaServices();
        }
        [HttpPost]
        public Venta CrearVenta([FromBody] Venta venta)
        {
            return _ventaServices.Crear(venta);
        }
        [HttpGet("usuario/{id}")]
        public List<Venta> GetVentasByIdUsuer(int idUsuario)
        {
            return _ventaServices.GetVentasByIdUsuer(idUsuario);
        }
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _ventaServices.Delete(id);
        }
        [HttpDelete("usuario/{id}")]
        public bool DeleteByIdUser(int id)
        {
            return _ventaServices.DeleteByIdUser(id);
        }
        [HttpGet("{id}")]
        public Venta Get(int id)
        {
            return _ventaServices.Get(id);
        }
        [HttpGet]
        public List<Venta> GetAll()
        {
            return _ventaServices.GetAll();
        }
        [HttpPut]
        public bool Update([FromBody]Venta entity)
        {
            return _ventaServices.Update(entity, Convert.ToInt32(entity.Id));
        }



    }
}
