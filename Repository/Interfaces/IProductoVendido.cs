using ProyectoFinalAPI_Antozzi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAPI_Antozzi.Repository.Interfaces
{
    public interface IProductoVendido: ICRUD<ProductoVendido>
    {
        /// <summary>
        ///    Se ingresa por parametro el NombreUsuario y devuelve una 
        ///    lista con los productos vendidos por ese esuario
        /// </summary>
        List<Producto> GetByUserName(string userName);
        /// <summary>
        ///    Se ingresa por parametro el Id del usuario y devuelve una 
        ///    lista con los productos vendidos por ese esuario
        /// </summary>
        List<Producto> GetByIdUsuario(Int64 userName);
    }
}
