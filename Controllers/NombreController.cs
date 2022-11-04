using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinalAPI_Antozzi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NombreController : ControllerBase
    {
        protected const string  MOMBREAPP = "ECommerce-APP";

        [HttpGet]
        public string TraerNombre() {
            return MOMBREAPP;
         }

    }

}
