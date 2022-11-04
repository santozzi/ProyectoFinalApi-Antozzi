using Microsoft.AspNetCore.Mvc;
using ProyectoFinalAPI_Antozzi.Entities;
using ProyectoFinalAPI_Antozzi.Repository.Exceptions;
using ProyectoFinalAPI_Antozzi.Services;

namespace ProyectoFinalAPI_Antozzi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {

        [HttpGet("{nombreUsuario}/{contraseña}")]
        public IActionResult InicioDeSesion(string nombreUsuario, string contraseña)
        {
            Usuario usuario = UsuarioServices.Instance().IsUsernameAndPassword(nombreUsuario, contraseña);
            if (usuario == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "El nombre de usuario y/o la contraseña son incorrectos");
            }
            else
            {
                return Ok(usuario);
            }

        }
        [HttpPost]
        [Produces("application/json", Type = typeof(Usuario))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CrearUsuario([FromBody] Usuario usuario)
        {
            Usuario usuarioCreado = null;
            try
            {
                usuarioCreado = UsuarioServices.Instance().Add(usuario);

            }
            catch (InvalidParametersException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);

            }
            return StatusCode(StatusCodes.Status201Created, usuarioCreado);
        }


        [HttpGet("{nombreUsuario}")]
        [Produces("application/json", Type = typeof(Usuario))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult TraerUsuario(string nombreUsuario)
        {

            Usuario usuario = UsuarioServices.Instance().GetByNombreUsuario(nombreUsuario);
            if (usuario == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "El usuario buscado no existe");
            }

            return Ok(usuario);
        }



        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ModificarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                UsuarioServices.Instance().Update(usuario, usuario.Id);
            }
            catch (InvalidParametersException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(true);
        }



        [HttpDelete("{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            try { 
              UsuarioServices.Instance().Delete(id);
            } catch (TheItemDoesNotExistException ex) { 
              return BadRequest(ex.Message);
            }
            return Ok(true);
        }


        /*
         * OTROS END POINT QUE  PODRIAN SER UTILIES
         * 
 

        [HttpGet("inicio")]
        public Usuario IniciarSesion([FromQuery] string usuario, [FromQuery] string clave)
        {
            return UsuarioServices.Instance().IsUsernameAndPassword(usuario, clave);
        }


        [HttpGet("{id}")]
        public Usuario Consultar(int id)
        {
            return UsuarioServices.Instance().Get(id);
        }
        */

    }
}
