using Microsoft.AspNetCore.Mvc;
using ProyectoFinalAPI_Antozzi.Entities;
using ProyectoFinalAPI_Antozzi.Repository.Exceptions;
using ProyectoFinalAPI_Antozzi.Services;

namespace ProyectoFinalAPI_Antozzi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : ControllerBase
    {
        



        
        [HttpPost("{idUsuario}")]
        public IActionResult CrearVenta([FromBody] List<Producto> productosVendidos, Int64 idUsuario)
        {
            try
            {
                VentaServices.Instance().Crear(productosVendidos, idUsuario);
            }
            catch (TheItemDoesNotExistException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound,ex.Message);
            }
            catch (InsufficientQuantityOfProductsException e) { 
                return StatusCode(StatusCodes.Status400BadRequest,e.Message);
            }
            return Created("", true); 
        }
        
        [HttpGet("usuario/{id}")]
        public List<Venta> GetVentasByIdUsuer(int idUsuario)
        {
            return VentaServices.Instance().GetVentasByIdUsuer(idUsuario);
        }
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return VentaServices.Instance().Delete(id);
        }
        [HttpDelete("usuario/{id}")]
        public bool DeleteByIdUser(int id)
        {
            return VentaServices.Instance().DeleteByIdUser(id);
        }
        [HttpGet("{id}")]
        public Venta Get(int id)
        {
            return VentaServices.Instance().Get(id);
        }
        [HttpGet]
        public List<Venta> GetAll()
        {
            return VentaServices.Instance().GetAll();
        }
        [HttpPut]
        public bool Update([FromBody]Venta entity)
        {
            return VentaServices.Instance().Update(entity, Convert.ToInt32(entity.Id));
        }



    }
}
