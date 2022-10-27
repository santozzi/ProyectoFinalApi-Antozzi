using Microsoft.AspNetCore.Mvc;
using ProyectoFinalAPI_Antozzi.Entities;
using ProyectoFinalAPI_Antozzi.Services;

namespace ProyectoFinalAPI_Antozzi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoServices _productoServices;
        public ProductoController()
        {
            _productoServices = new ProductoServices();
        }

        [HttpGet("usuario/{id}")]
        public List<Producto> GetProductoByIdUsuario(int id)
        {

            List<Producto> productos = _productoServices.GetByIdUsuario(id);
            return productos;

        }
        [HttpGet("{id}")]
        public Producto Get(int id)
        {
            return _productoServices.Get(id);
        }
        [HttpGet]
        public List<Producto> GetAll()
        {
            return _productoServices.GetAll();
        }

        [HttpPost]
        public Producto Crear([FromBody] Producto entity)
        {
            return _productoServices.Add(entity);
        }

        [HttpPut]
        public bool Modificar([FromBody] Producto entity)
        {
            return _productoServices.Update(entity, Convert.ToInt32(entity.Id));
        }
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _productoServices.Delete(id);
        }
    }
}
