using Microsoft.AspNetCore.Mvc;
using ProyectoFinalAPI_Antozzi.Entities;
using ProyectoFinalAPI_Antozzi.Repository.Exceptions;
using ProyectoFinalAPI_Antozzi.Services;

namespace ProyectoFinalAPI_Antozzi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
       
 

        [HttpGet("usuario/{id}")]
        public IEnumerable<Producto> GetProductoByIdUsuario(int id)
        {

            IEnumerable<Producto> productos = ProductoServices.Instance().GetByIdUsuario(id);


            return productos;

        }

        [HttpGet("{id}")]
        [Produces("application/json",Type = typeof(Producto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(Int64 id)
        {

            
          Producto producto = null;
            try
            {
                producto= ProductoServices.Instance().Get(id);
            }
            catch (TheItemDoesNotExistException ex) {

                return StatusCode(StatusCodes.Status404NotFound,ex.Message);
              


            }

            return Ok(producto);
        }
        [HttpGet]
        public List<Producto> GetAll()
        {
            return ProductoServices.Instance().GetAll();
        }

        [HttpPost]
        [Produces("application/json", Type = typeof(Producto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created,Type= typeof(Producto))]
        public IActionResult Crear([FromBody] Producto entity)
        {
            Producto res = null;
            try { 
                res = ProductoServices.Instance().Add(entity);
            } catch (TheItemDoesNotExistException ex) {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }catch (InvalidParametersException e) {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            // 


            return Created("",res);
        }

        [HttpPut]
        public bool Modificar([FromBody] Producto entity)
        {
            return ProductoServices.Instance().Update(entity, Convert.ToInt32(entity.Id));
        }
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return ProductoServices.Instance().Delete(id);
        }
    }
}
