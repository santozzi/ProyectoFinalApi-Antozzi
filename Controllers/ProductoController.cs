using Microsoft.AspNetCore.Mvc;
using ProyectoFinalAPI_Antozzi.Entities;
using ProyectoFinalAPI_Antozzi.Repository.Exceptions;
using ProyectoFinalAPI_Antozzi.Services;
using System.Collections.Generic;

namespace ProyectoFinalAPI_Antozzi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {


        [HttpPost]
        [Produces("application/json", Type = typeof(Producto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Producto))]
        public IActionResult CrearProducto([FromBody] Producto entity)
        {
            Producto res = null;
            try
            {
                res = ProductoServices.Instance().Add(entity);
            }
            catch (TheItemDoesNotExistException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (InvalidParametersException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            


            return StatusCode(StatusCodes.Status201Created,res);
        }
        [HttpPut]
        public IActionResult ModificarProducto([FromBody] Producto entity)
        {
            try
            {
                ProductoServices.Instance().Update(entity, Convert.ToInt32(entity.Id));
            }
            catch (TheItemDoesNotExistException ex) { 
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }

            return Ok(true);
        }



        [HttpDelete("{id}")]
        public IActionResult EliminarProducto(Int64 id)
        {
            try {
                 ProductoServices.Instance().Delete(id);
            }catch(TheItemDoesNotExistException ex) {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            return Ok(true);
        }


        [HttpGet("{idUsuario}")]
        public IActionResult TraerProductos(Int64 idUsuario)
        {
            List<Producto> list = null;
            try
            {
                list = ProductoServices.Instance().GetByIdUsuario(idUsuario);
            }
            catch (TheItemDoesNotExistException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            return Ok(list);
        }
        [HttpGet]
        [Produces("application/json", Type = typeof(List<Producto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult TraerTodosLosProductos()
        {


            List<Producto> productos = null;
            try
            {
                productos = ProductoServices.Instance().GetAll();
            }
            catch (TheItemDoesNotExistException ex)
            {

                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }

            return Ok(productos);
        }


        /*
         * 
         * OTROS ENDPOINTS UTILES
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
  


        */
    }
}
