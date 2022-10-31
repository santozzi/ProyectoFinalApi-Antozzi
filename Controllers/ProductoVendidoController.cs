using Microsoft.AspNetCore.Mvc;
using ProyectoFinalAPI_Antozzi.Entities;
using ProyectoFinalAPI_Antozzi.Services;

namespace ProyectoFinalAPI_Antozzi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoVendidoController : ControllerBase
    {
       

        [HttpPost]
        public ProductoVendido Add([FromBody]ProductoVendido entity)
        {
            return ProductoVendidoServices.Instance().Add(entity);
        }
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return ProductoVendidoServices.Instance().Delete(id);
        }
        [HttpGet("{id}")]
        public ProductoVendido Get(int id)
        {
            return ProductoVendidoServices.Instance().Get(id);
        }
        [HttpGet]
        public List<ProductoVendido> GetAll()
        {
            return ProductoVendidoServices.Instance().GetAll();
        }
        [HttpGet("usuario/{id}")]
        public List<Producto> GetByIdUsuario(Int64 id)
        {
            return ProductoVendidoServices.Instance().GetByIdUsuario(id);
        }
        [HttpPut]
        public bool Update([FromBody]ProductoVendido entity)
        {
            return ProductoVendidoServices.Instance().Update(entity, Convert.ToInt32(entity.Id));
        }
        [HttpGet("usuario/name")]
        public List<Producto> GetByUserName([FromQuery]string userName)
        {
            return ProductoVendidoServices.Instance().GetByUserName(userName);
        }

    }
}
