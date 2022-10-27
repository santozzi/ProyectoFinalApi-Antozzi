using Microsoft.AspNetCore.Mvc;
using ProyectoFinalAPI_Antozzi.Entities;
using ProyectoFinalAPI_Antozzi.Services;

namespace ProyectoFinalAPI_Antozzi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioServices _usuarioServices;
        public UsuarioController() {
            _usuarioServices = new UsuarioServices();
        }
        [HttpGet("inicio")]
        public Usuario IniciarSesion([FromQuery] string usuario, [FromQuery] string clave)
        {
            return _usuarioServices.IsUsernameAndPassword(usuario, clave);
        }

        [HttpGet]
        public List<Usuario> Consultar()
        {
            return _usuarioServices.GetAll();
        }
        [HttpGet("{id}")]
        public Usuario Consultar(int id)
        {
            return _usuarioServices.Get(id);
        }
   
        [HttpPost]
        public Usuario Crear([FromBody] Usuario usuario)
        {
            return _usuarioServices.Add(usuario);
        }
        [HttpPut]
        public bool Actualizar([FromBody] Usuario usuario)
        {
            return _usuarioServices.Update(usuario,Convert.ToInt32(usuario.Id));
        }
        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            return _usuarioServices.Delete(id);
        }


       
        


    
    }
}
