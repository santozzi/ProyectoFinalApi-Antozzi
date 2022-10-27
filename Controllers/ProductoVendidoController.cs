using Microsoft.AspNetCore.Mvc;
using ProyectoFinalAPI_Antozzi.Entities;
using ProyectoFinalAPI_Antozzi.Services;

namespace ProyectoFinalAPI_Antozzi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoVendidoController : ControllerBase
    {
        protected readonly ProductoVendidoServices _productoVendidoServices;
        public ProductoVendidoController()
        {
            _productoVendidoServices = new ProductoVendidoServices();
        }
        [HttpPost]
        public ProductoVendido Add([FromBody]ProductoVendido entity)
        {
            return _productoVendidoServices.Add(entity);
        }
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _productoVendidoServices.Delete(id);
        }
        [HttpGet("{id}")]
        public ProductoVendido Get(int id)
        {
            return _productoVendidoServices.Get(id);
        }
        [HttpGet]
        public List<ProductoVendido> GetAll()
        {
            return _productoVendidoServices.GetAll();
        }
        [HttpGet("usuario/{id}")]
        public List<Producto> GetByIdUsuario(Int64 id)
        {
            return _productoVendidoServices.GetByIdUsuario(id);
        }
        [HttpPut]
        public bool Update([FromBody]ProductoVendido entity)
        {
            return _productoVendidoServices.Update(entity, Convert.ToInt32(entity.Id));
        }
        [HttpGet("usuario/name")]
        public List<Producto> GetByUserName([FromQuery]string userName)
        {
            return _productoVendidoServices.GetByUserName(userName);
        }

    }
}
