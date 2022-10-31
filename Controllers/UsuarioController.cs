using Microsoft.AspNetCore.Mvc;
using ProyectoFinalAPI_Antozzi.Entities;
using ProyectoFinalAPI_Antozzi.Services;

namespace ProyectoFinalAPI_Antozzi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
       
 
        [HttpGet("inicio")]
        public Usuario IniciarSesion([FromQuery] string usuario, [FromQuery] string clave)
        {
            return UsuarioServices.Instance().IsUsernameAndPassword(usuario, clave);
        }

        [HttpGet]
        public List<Usuario> Consultar()
        {
            return UsuarioServices.Instance().GetAll();
        }
        [HttpGet("{id}")]
        public Usuario Consultar(int id)
        {
            return UsuarioServices.Instance().Get(id);
        }
   
        [HttpPost]
        public Usuario Crear([FromBody] Usuario usuario)
        {
            return UsuarioServices.Instance().Add(usuario);
        }
        [HttpPut]
        public bool Actualizar([FromBody] Usuario usuario)
        {
            return UsuarioServices.Instance().Update(usuario,Convert.ToInt32(usuario.Id));
        }
        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            return UsuarioServices.Instance().Delete(id);
        }


       
        


    
    }
}
