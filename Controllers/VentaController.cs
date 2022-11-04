using Microsoft.AspNetCore.Mvc;
using ProyectoFinalAPI_Antozzi.Entities;
using ProyectoFinalAPI_Antozzi.Repository.Exceptions;
using ProyectoFinalAPI_Antozzi.Services;

namespace ProyectoFinalAPI_Antozzi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {





        [HttpPost("{idUsuario}")]
        public IActionResult CargarVenta([FromBody] List<Producto> productosVendidos, Int64 idUsuario)
        {
            try
            {
                VentaServices.Instance().Crear(productosVendidos, idUsuario);
            }
            catch (TheItemDoesNotExistException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (InsufficientQuantityOfProductsException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            return StatusCode(StatusCodes.Status201Created, true);
        }

        [HttpDelete]
        public IActionResult Delete(Venta venta)
        {
            try
            {
                VentaServices.Instance().DeleteVenta(venta);
            }
            catch (TheItemDoesNotExistException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            return Ok(true);
        }

        [HttpGet("{idUsuario}")]
        [Produces("application/json", Type = typeof(List<VentaConProducto>))]
        public IActionResult TraerVentas(Int64 idUsuario)
        {
            List<VentaConProducto> respuesta = null;
            try
            {
                respuesta = VentaServices.Instance().TraerVenta(idUsuario);
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest,ex.Message);
            }
            return Ok(respuesta);
        }
        [HttpGet]
        public List<Venta> GetAll()
        {
            return VentaServices.Instance().GetAll();
        }


        [HttpPut]
        public bool Update([FromBody] Venta entity)
        {
            return VentaServices.Instance().Update(entity, Convert.ToInt32(entity.Id));
        }


        /*
        [HttpDelete("usuario/{id}")]
        public bool DeleteByIdUser(int id)
        {
            return VentaServices.Instance().DeleteByIdUser(id);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            List<Producto> prod = null;
            //List<Venta> ventas = null;
            try
            {

                //ventas =  VentaServices.Instance().GetVentasByIdUsuer(id);
                prod = ProductoVendidoServices.Instance().GetByIdUsuario(id);
            }
            catch (TheItemDoesNotExistException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

            return Ok(prod);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Int64 id)
        {
            try
            {
                VentaServices.Instance().Delete(id);
            }
            catch (TheItemDoesNotExistException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            return Ok(true);
        }
        */

    }
}
